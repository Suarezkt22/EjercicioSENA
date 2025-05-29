using MediatR;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

public record struct GetCoursesPerProgramQuery(int ProgramId) : IRequest<Response<List<GetCoursesPerProgramResponse>>>;