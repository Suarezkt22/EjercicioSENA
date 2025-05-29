using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands.Handlers;

public class RegisterStudentCommandHandler(IStudentRepository _studentRepository, IUnitOfWork _unitOfWork) : IRequestHandler<RegisterStudentCommand, Response<string>>
{
    public async Task<Response<string>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        await EnsureStudentDoesNotExist(request.Name, cancellationToken);

        var newStudent = Student.Create(request.Name);

        await CreateStudent(newStudent, cancellationToken);

        return new Response<string>($"El estudiante {newStudent.Name} ha sido registrado correctamente.");
    }

    private async Task EnsureStudentDoesNotExist(string name, CancellationToken cancellationToken)
    {
        var exists = await _studentRepository.ExistsByName(name, cancellationToken);

        if (exists) throw new GeneralException($"Ya existe un estudiante registrado con el nombre '{name}'.");
    }

    private async Task CreateStudent(Student student, CancellationToken cancellationToken)
    {
        await _studentRepository.Create(student, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}