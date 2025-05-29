using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Models.Employee;
using PruebaTecnicaInterrapidisimo.Models.Employee.Repositories;
using PruebaTecnicaInterrapidisimo.Models.Employee.Services;
using Moq;

namespace PruebaTecnicaInterrapidisimo.Tests.Tests;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfEmployees()
    {
        var employees = new List<Employee>
        {
            new() { Id = 1, Name = "John Doe", Age = 30, Salary = 2000, ProfileImg = "john.jpg" },
            new() { Id = 2, Name = "Jane Doe", Age = 25, Salary = 3000, ProfileImg = "jane.jpg" }
        };

        _employeeRepositoryMock
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(employees);

        var result = await _employeeService.GetAll();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(24000, result.Data[0].AnualSalary); // 2000 * 12
        Assert.Equal(36000, result.Data[1].AnualSalary); // 3000 * 12
    }

    [Fact]
    public async Task GetById_ShouldReturnEmployee_WhenEmployeeExists()
    {
        // Arrange
        Employee employee = new() { Id = 1, Name = "John Doe", Age = 30, Salary = 2000, ProfileImg = "john.jpg" };

        _employeeRepositoryMock
            .Setup(repo => repo.GetById(1))
            .ReturnsAsync(employee);

        // Act
        var result = await _employeeService.GetById(1);

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal("John Doe", result.Data.Name);
    }

    [Fact]
    public async Task GetById_ShouldThrowNotFoundException_WhenEmployeeDoesNotExist()
    {
        _employeeRepositoryMock
            .Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync((Employee)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _employeeService.GetById(999));
        Assert.Equal("El empleado con ID 999 no fue encontrado.", exception.Message);
    }
}
