using Microsoft.Extensions.Configuration;
using PruebaTecnicaAmaris.Common.Abstract;

namespace PruebaTecnicaAmaris.Common.Clients;

public class EmployeeAPIConfigClient(IConfiguration configuration) : ConfigClient(configuration) {

    public string BaseUrl => GetValue<string>("EmployeeApi:BaseUrl");
    public string GetAllEndpoint => GetValue<string>("EmployeeApi:GetAllEndpoint");
    public string GetByIdEndpoint(int id) => $"{GetValue<string>("EmployeeApi:GetById")}/{id}";
}
