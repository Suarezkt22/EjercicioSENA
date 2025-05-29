using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands.Handlers;

public class EnrollCoursesCommandHandler(
    IStudentRepository _studentRepository,
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork
)
    : IRequestHandler<EnrollCoursesCommand, Response<string>>
{
    public async Task<Response<string>> Handle(EnrollCoursesCommand request, CancellationToken cancellationToken)
    {
        var student = await GetStudentAsync(request.StudentId, cancellationToken);

        var coursesToEnroll = await GetCoursesAsync(request.CoursesIds, cancellationToken);

        CheckMissingCourses(request.CoursesIds, coursesToEnroll);

        student.EnrollCourses(coursesToEnroll);

        await UpdateStudent(student, cancellationToken);

        return new Response<string>(
            $"El estudiante {student.Name} ha sido inscrito en {coursesToEnroll.Count} curso(s) correctamente.");
    }

    private async Task UpdateStudent(Student student , CancellationToken cancellationToken)
    {
        await _studentRepository.Update(student, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<Student> GetStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetById(studentId, cancellationToken)
            ?? throw new GeneralException($"El estudiante indicado no existe.");
    }

    private async Task<List<Course>> GetCoursesAsync(List<int> courseIds, CancellationToken cancellationToken)
    {
        return await _courseRepository.GetByIds(courseIds, cancellationToken);
    }

    private static void CheckMissingCourses(List<int> coursesIds , List<Course> coursesToEnroll)
    {
        var missingCourseIds = coursesIds.Except(coursesToEnroll.Select(c => c.Id)).ToList();

        if (missingCourseIds.Count != 0)
        {
            throw new GeneralException($"No se encontraron los siguientes cursos: {string.Join(", ", missingCourseIds)}.");
        }

    }
}
