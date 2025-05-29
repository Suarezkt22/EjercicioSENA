using Carter;
using Microsoft.Extensions.Options;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;
using PruebaTecnicaInterrapidisimo.Infraestructure.Repositories;

namespace PruebaTecnicaInterrapidisimo;

public static class DependencyContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
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

        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
