using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;

public record struct RegisterStudentCommand(string Name) : IRequest<Response<int>>;