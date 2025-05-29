using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;

public record struct ApplyProgramCommand(int StudentId, int ProgramId) : IRequest<Response<string>>;