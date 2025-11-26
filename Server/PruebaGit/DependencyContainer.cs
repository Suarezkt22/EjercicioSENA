using Carter;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Infraestructure.Persistence;
using GitEjercicioSENA.Infraestructure.Repositories.Dapper;
using GitEjercicioSENA.Infraestructure.Services;

namespace GitEjercicioSENA;

public static class DependencyContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // ✔ Creamos una conexión SQLite para Dapper
        services.AddScoped(provider =>
        {
            var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;

            var connection = new SqliteConnection(appSettings.DbConnectionString);
            connection.Open();
            return connection;
        });

        // ✔ DbContext escritura
        services.AddDbContext<DbWriteContext>((serviceProvider, options) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            DbContextOptionSetup.ConfigureWriteOptions(options, appSettings.DbConnectionString);
        });

        // ✔ DbContext lectura
        services.AddDbContext<DbReadContext>((serviceProvider, options) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            DbContextOptionSetup.ConfigureReadOptions(options, appSettings.DbConnectionString);
        });

        services.AddCarter();

        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, TokenService>();

        // ✔ Repositorios Dapper funcionando con SQLite
        services.AddScoped<IProductRepository, DapperProductRepository>();
        services.AddScoped<IUserRepository, DapperUserRepository>();
        services.AddScoped<IUnitOfWork, DapperUnitOfWork>();

        return services;
    }
}