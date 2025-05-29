namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;

public record struct GetAllProgramsResponse
{
    public int ProgramId { get; set; }
    public string Name { get; set; }
}
