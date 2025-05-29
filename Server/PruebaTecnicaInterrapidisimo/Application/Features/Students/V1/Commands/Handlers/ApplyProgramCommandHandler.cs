using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands.Handlers;

public class ApplyProgramCommandHandler(
    IStudentRepository _studentRepository,
    IProgramRepository _programRepository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<ApplyProgramCommand, Response<string>>
{
    public async Task<Response<string>> Handle(ApplyProgramCommand request, CancellationToken cancellationToken)
    {
        var student = await GetStudentAsync(request.StudentId, cancellationToken);

        var program = await GetProgramAsync(request.ProgramId, cancellationToken);

        student.ApplyProgram(program);

        await UpdateStudent(student, cancellationToken);

        return new Response<string>($"El estudiante {student.Name} se ha registrado al programa {program.Name} correctamente.");
    }

    private async Task UpdateStudent(Student student , CancellationToken cancellationToken)
    {
        await _studentRepository.Update(student, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<Student> GetStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        return await _studentRepository.GetById(studentId, cancellationToken) ??
        throw new GeneralException($"El estudiante indicado no existe.");
    }

    private async Task<DomainProgram> GetProgramAsync(int programId, CancellationToken cancellationToken)
    {
        return await _programRepository.GetById(programId, cancellationToken) ??
        throw new GeneralException($"El programa indicado no existe.");
    }
}