using Carter;
using PruebaTecnicaInterrapidisimo;
using Spectre.Console;
using Color = Spectre.Console.Color;
using DotNetEnv;
using PruebaTecnicaInterrapidisimo.Common.Exceptions.Configuration;
using PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables().Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod(); 
    });
});

builder.Services.AddServices();

builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

var connectionString = configuration.GetValue<string>(key: "DBCONNECTIONSTRING");

builder.Services.Configure<AppSettings>(appSettings =>
{
    appSettings.DbConnectionString = connectionString!;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentsService");
        options.RoutePrefix = string.Empty;
    });
}

app.MapCarter();

app.UseCors("AllowSpecificOrigins");

app.UseRouting();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

DisplayAppName("StudentsService");

app.Run();

static void DisplayAppName(string appName)
{
    var figletText = new FigletText(appName).Centered().Color(Color.Aqua);
    AnsiConsole.Write(figletText);
}