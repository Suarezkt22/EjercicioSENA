using Carter;
using Spectre.Console;
using Color = Spectre.Console.Color;
using DotNetEnv;
using GitEjercicioSENA.Common.Exceptions.Configuration;
using GitEjercicioSENA.Infraestructure.Persistence;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GitEjercicioSENA;
using Microsoft.EntityFrameworkCore;

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

// Configuración Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "PruebaGitSENA API",
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

// Configuración de SQLite
var connectionString = "Data Source=database.db";
builder.Services.Configure<AppSettings>(appSettings =>
{
    appSettings.DbConnectionString = connectionString!;
});

builder.Services.AddDbContext<DbReadContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<DbWriteContext>(options =>
    options.UseSqlite(connectionString));

// Configuración JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Intentar leer desde appsettings.json y env vars
var jwtKey = jwtSettings["Key"]
             ?? Environment.GetEnvironmentVariable("JWT__KEY")
             ?? Environment.GetEnvironmentVariable("Jwt__Key");

// Evitar null en producción
if (string.IsNullOrWhiteSpace(jwtKey))
    jwtKey = "fallback_key_123_change_me";

var key = Encoding.UTF8.GetBytes(jwtKey);

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

// 🔥 EJECUTAR MIGRACIONES AUTOMÁTICAMENTE (ESSENCIAL PARA RAILWAY)
using (var scope = app.Services.CreateAsyncScope())
{
    var dbRead = scope.ServiceProvider.GetRequiredService<DbReadContext>();
    var dbWrite = scope.ServiceProvider.GetRequiredService<DbWriteContext>();

    await dbRead.Database.MigrateAsync();
    await dbWrite.Database.MigrateAsync();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Swagger siempre activo (Railway = Production)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API");
    options.RoutePrefix = "swagger";
});

// app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapCarter();

DisplayAppName("Prueba API");

await app.RunAsync();

static void DisplayAppName(string appName)
{
    var figletText = new FigletText(appName).Centered().Color(Color.Aqua);
    AnsiConsole.Write(figletText);
}