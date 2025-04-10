using Carter;
using PruebaTecnicaAmaris;
using Spectre.Console;
using Color = Spectre.Console.Color;
using DotNetEnv;
using PruebaTecnicaAmaris.Common.Exceptions.Configuration;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables().Build();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(config =>
        config
        .AllowAnyHeader()
        .WithMethods("GET")
        .WithOrigins("http://localhost:5001")
       ));

builder.Services.AddServices();

builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeesService");
        options.RoutePrefix = string.Empty;
    });
}

app.MapCarter();

app.UseCors();

app.UseRouting();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

DisplayAppName("EmployeesMVC");

app.Run();

static void DisplayAppName(string appName)
{
    var figletText = new FigletText(appName).Centered().Color(Color.Aqua);
    AnsiConsole.Write(figletText);
}