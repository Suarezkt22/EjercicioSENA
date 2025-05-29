using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class GetEnrolledCoursesEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ApiTags.ApiBaseRoute}/{ApiTags.StudentsTag}/enrolled-courses", async (
            [FromQuery] int studentId,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(new GetEnrolledCoursesQuery(studentId)));
        })
        .WithTags(ApiTags.StudentsTag)
        .WithName("GetEnrolledCourses");
    }
}
