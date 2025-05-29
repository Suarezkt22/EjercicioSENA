namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;

public record struct GetCoursesResponse
{
    public int CourseId { get; set; }
    public string Name { get; set; }
    public int Credits { get; set; }
    public TeacherDto? Teacher { get; set; }

}

public record struct TeacherDto
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
}