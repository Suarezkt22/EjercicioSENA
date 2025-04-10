using Carter;
using PruebaTecnicaAmaris.Controllers.Tags;
using PruebaTecnicaAmaris.Models.Employee.Services;

namespace PruebaTecnicaAmaris.Controllers;

public class EmployeeController(EmployeeService employeeService) : CarterModule
{
    private readonly EmployeeService _employeeService = employeeService;

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{EmployeeControllerTags.ApiBaseRoute}/{EmployeeControllerTags.Tag}/all", async () =>
        {
            var result = await _employeeService.GetAll();
            return Results.Ok(result);
        });

        app.MapGet($"{EmployeeControllerTags.ApiBaseRoute}/{EmployeeControllerTags.Tag}/{{id}}", async (int id) =>
        {
            var result = await _employeeService.GetById(id);
            return Results.Ok(result);
        });
    }
}
