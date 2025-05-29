using MediatR;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries.Handlers;

public class GetEnrolledCoursesQueryHandler(IStudentRepository _studentRepository)
    : IRequestHandler<GetEnrolledCoursesQuery, Response<List<GetCoursesResponse>>>
{
    public async Task<Response<List<GetCoursesResponse>>> Handle(GetEnrolledCoursesQuery request, CancellationToken cancellationToken)
    {
        var student = await GetStudentAsync(request.StudentId, cancellationToken);

        var response = GetCoursesPerProgramResponse([.. student.EnrolledCourses]);

        return new Response<List<GetCoursesResponse>>(response);
    }

    private static List<GetCoursesResponse> GetCoursesPerProgramResponse(List<Course> courses)
    {

        return [.. courses
            .Select(course => new GetCoursesResponse
            {
                CourseId = course.Id,
                Name = course.Name,
                Teacher = course.Teacher != null
                    ? new TeacherDTO
                    {
                        TeacherId = course.Teacher.Id,
                        Name = course.Teacher.Name
                    }
                    : null
            })];
    }

    private async Task<Student> GetStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetById(studentId, cancellationToken)
            ?? throw new GeneralException($"El estudiante indicado no existe.");
    }

}