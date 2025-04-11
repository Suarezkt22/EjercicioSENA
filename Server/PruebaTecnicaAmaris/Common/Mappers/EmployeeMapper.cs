using PruebaTecnicaAmaris.Models.Employee;
using PruebaTecnicaAmaris.Models.Employee.Repositories.DTOs;

namespace PruebaTecnicaAmaris.Common.Mappers;

public class EmployeeMapper
{
    public static Employee ToEntity(APIEmployeeDTO employeeDTO)
    {
        return new Employee {
            Id = employeeDTO.Id, 
            Name = employeeDTO.EmployeeName, 
            Age = employeeDTO.EmployeeAge, 
            Salary = employeeDTO.EmployeeSalary, 
            ProfileImg = employeeDTO.ProfileImage 
        };
    }
}
