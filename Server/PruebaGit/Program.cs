using Carter;
using Spectre.Console;
using Color = Spectre.Console.Color;
using DotNetEnv;
using PruebaRaddarStudios.Common.Exceptions.Configuration;
using PruebaRaddarStudios.Infraestructure.Persistence;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PruebaRaddarStudios;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración desde .env
Env.Load();

// Configuración de appsettings.json y variables de entorno
var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuración de servicios
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();

// Configuracion Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "PruebaRaddarStudios API",
        Version = "v1"
    });

    // Configuración de autenticación JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {tu_token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Configuración de la conexión a la base de datos
var connectionString = configuration.GetValue<string>("DBCONNECTIONSTRING");
builder.Services.Configure<AppSettings>(appSettings =>
{
    appSettings.DbConnectionString = connectionString!;
});



// Configuración de JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API");
        options.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapCarter();

DisplayAppName("Prueba API");

await app.RunAsync();

static void DisplayAppName(string appName)
{
    var figletText = new FigletText(appName).Centered().Color(Color.Aqua);
    AnsiConsole.Write(figletText);
}
