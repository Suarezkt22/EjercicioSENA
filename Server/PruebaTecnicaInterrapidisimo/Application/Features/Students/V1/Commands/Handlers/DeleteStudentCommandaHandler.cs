using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands.Handlers;

public class DeleteStudentCommandaHandler(IStudentRepository _studentRepository, IUnitOfWork _unitOfWork) : IRequestHandler<DeleteStudentCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await GetStudentAsync(request.StudentId, cancellationToken);

        await DeleteStudent(student, cancellationToken);

        return new Response<string>($"El estudiante {student.Name} ha sido eliminado correctamente.");
    }

    private async Task<Student> GetStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetById(studentId, cancellationToken)
            ?? throw new GeneralException($"El estudiante indicado no existe.");
    }

    private async Task DeleteStudent(Student student , CancellationToken cancellationToken)
    {
        await _studentRepository.Delete(student, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}