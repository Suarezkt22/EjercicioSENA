using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;

public record struct EnrollCoursesCommand(int StudentId , List<int> CoursesIds) : IRequest<Response<string>>;