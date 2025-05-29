using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;

public record struct DeleteStudentCommand(int StudentId) : IRequest<Response<string>>;