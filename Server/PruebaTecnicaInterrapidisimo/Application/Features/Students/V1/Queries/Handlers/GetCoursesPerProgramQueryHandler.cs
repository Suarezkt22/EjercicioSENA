using MediatR;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries.Handlers;

public class GetCoursesPerProgramQueryHandler(ICourseRepository _courseRepository, IProgramRepository _programRepository)
    : IRequestHandler<GetCoursesPerProgramQuery, Response<List<GetCoursesResponse>>>
{
    public async Task<Response<List<GetCoursesResponse>>> Handle(GetCoursesPerProgramQuery request, CancellationToken cancellationToken)
    {
        var program = await GetProgramAsync(request.ProgramId, cancellationToken);

        var courses = await _courseRepository.GetByProgram(program, cancellationToken);

        var response = GetCoursesPerProgramResponse(courses);

        return new Response<List<GetCoursesResponse>>(response);
    }

    private static List<GetCoursesResponse> GetCoursesPerProgramResponse(List<Course> courses)
    {

        return [.. courses
            .Select(course => new GetCoursesResponse
            {
                CourseId = course.Id,
                Name = course.Name,
                Credits = course.Credits,
                Teacher = course.Teacher != null
                    ? new TeacherDto
                    {
                        TeacherId = course.Teacher.Id,
                        Name = course.Teacher.Name
                    }
                    : null
            })];
    }

    private async Task<DomainProgram> GetProgramAsync(int programId, CancellationToken cancellationToken)
    {
        return await _programRepository.GetById(programId, cancellationToken) ??
        throw new GeneralException($"El programa indicado no existe.");
    }

}