namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;

public record struct GetCoursesPerProgramResponse
{
    public int CourseId { get; set; }
    public string Name { get; set; }
    public TeacherDTO? Teacher { get; set; }

}

public record struct TeacherDTO
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
}