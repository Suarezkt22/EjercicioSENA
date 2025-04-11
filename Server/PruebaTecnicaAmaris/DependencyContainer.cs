using Carter;
using PruebaTecnicaAmaris.Common.Clients;
using PruebaTecnicaAmaris.Models.Employee.Repositories;
using PruebaTecnicaAmaris.Models.Employee.Services;

namespace PruebaTecnicaAmaris;

public static class DependencyContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddCarter();

        services.AddHttpContextAccessor();

        services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

        services.AddSingleton<EmployeeAPIConfigClient>();

        services.AddSingleton<EmployeeService>();

        return services;
    }
}
