using MediatR;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.DTOs;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

public record struct GetEnrolledCoursesQuery(int StudentId) : IRequest<Response<List<GetCoursesResponse>>>;