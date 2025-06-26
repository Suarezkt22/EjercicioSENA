using Carter;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Infraestructure.Persistence;
using PruebaRaddarStudios.Infraestructure.Repositories;
using PruebaRaddarStudios.Infraestructure.Repositories.Dapper;

using PruebaRaddarStudios.Infraestructure.Services;

namespace PruebaRaddarStudios;

public static class DependencyContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
            var connection = new SqlConnection(appSettings.DbConnectionString);
            connection.Open();
            return connection;
        });

        services.AddDbContext<DbWriteContext>((serviceProvider, options) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            DbContextOptionSetup.ConfigureWriteOptions(options, appSettings.DbConnectionString);
        });

        services.AddDbContext<DbReadContext>((serviceProvider, options) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            DbContextOptionSetup.ConfigureReadOptions(options, appSettings.DbConnectionString);
        });

        services.AddCarter();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IProductRepository, DapperProductRepository>();
        services.AddScoped<IUserRepository, DapperUserRepository>();
        services.AddScoped<IUnitOfWork, DapperUnitOfWork>();

        return services;
    }
}
