using System.Text.Json.Serialization;

namespace PruebaTecnicaAmaris.Models.Employee.Repositories.DTOs;

public class ApiEmployeeWrapper<T>
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public required T? Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class APIEmployeeDTO
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("employee_name")]
    public string EmployeeName { get; set; } = string.Empty;

    [JsonPropertyName("employee_salary")]
    public int EmployeeSalary { get; set; }

    [JsonPropertyName("employee_age")]
    public int EmployeeAge { get; set; }

    [JsonPropertyName("profile_image")]
    public string ProfileImage { get; set; } = string.Empty;

}
