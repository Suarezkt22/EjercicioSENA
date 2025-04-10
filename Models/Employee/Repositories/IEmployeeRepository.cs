namespace PruebaTecnicaAmaris.Models.Employee.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAll();
    Task<Employee?> GetById(int id);
}
