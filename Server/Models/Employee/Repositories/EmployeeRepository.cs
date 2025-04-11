using PruebaTecnicaAmaris.Common.Clients;
using PruebaTecnicaAmaris.Common.Mappers;
using PruebaTecnicaAmaris.Models.Employee.Repositories.DTOs;
using RestSharp;

namespace PruebaTecnicaAmaris.Models.Employee.Repositories;

public class EmployeeRepository(EmployeeAPIConfigClient configClient) : IEmployeeRepository
{
    private readonly EmployeeAPIConfigClient _configClient = configClient;

    public async Task<List<Employee>> GetAll()
    {
        await Task.Delay(30000); // Api very restrictive with 429 too many requests

        var response = await APIClient.ExecuteAsync<ApiEmployeeWrapper<List<APIEmployeeDTO>>>(
            _configClient.BaseUrl,
            _configClient.GetAllEndpoint,
            Method.Get
        );

        var employees = response?.Data?.ConvertAll(EmployeeMapper.ToEntity) ?? [];

        return employees;
    }

    public async Task<Employee?> GetById(int id)
    {
        await Task.Delay(30000); // Api very restrictive with 429 too many requests
        
        var response = await APIClient.ExecuteAsync<ApiEmployeeWrapper<APIEmployeeDTO>>(
            _configClient.BaseUrl,
            _configClient.GetByIdEndpoint(id),
            Method.Get
        );

        if (response?.Data is null) return null;

        return EmployeeMapper.ToEntity(response.Data);
    }

}
