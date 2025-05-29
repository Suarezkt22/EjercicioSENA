using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Wrappers;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates.ValueObjects;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

public record struct GetClassmatesPerCourseQuery(int StudentId, int CourseId) : IRequest<Response<ClassmatesInfo>>;