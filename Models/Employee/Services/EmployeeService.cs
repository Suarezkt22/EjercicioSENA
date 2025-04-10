using PruebaTecnicaAmaris.Common.Exceptions;
using PruebaTecnicaAmaris.Common.Wrappers;
using PruebaTecnicaAmaris.Models.Employee.Repositories;

namespace PruebaTecnicaAmaris.Models.Employee.Services;

public class EmployeeService(IEmployeeRepository employeeRepository)
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Response<List<Employee>>> GetAll()
    {
        var result = await _employeeRepository.GetAll();

        return new Response<List<Employee>>(result);
    }

    public async Task<Response<Employee>> GetById(int id)
    {
        var result = await _employeeRepository.GetById(id) ?? throw new NotFoundException($"El empleado con ID {id} no fue encontrado.");

        return new Response<Employee>(result);
    }

}
