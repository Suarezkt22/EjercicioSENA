using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates.ValueObjects;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Domain.Services;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries.Handlers;

public class GetClassmatesPerCourseQueryHandler(IStudentRepository _studentRepository) : IRequestHandler<GetClassmatesPerCourseQuery, Response<ClassmatesInfo>>
{
    public async Task<Response<ClassmatesInfo>> Handle(GetClassmatesPerCourseQuery request, CancellationToken cancellationToken)
    {
        var student = await GetStudentAsync(request.StudentId, cancellationToken);

        var classMates = ClassmatesService.GetClassmates(student, request.CourseId);

        return new Response<ClassmatesInfo>(classMates);
    }
    
    private async Task<Student> GetStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetById(studentId, cancellationToken)
            ?? throw new GeneralException($"El estudiante indicado no existe.");
    }
}