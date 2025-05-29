using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class GetClassmatesPerCourseEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ApiTags.ApiBaseRoute}/{ApiTags.StudentsTag}/classmates", async (
            [FromQuery] int studentId,
            [FromQuery] int courseId,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(new GetClassmatesPerCourseQuery(studentId, courseId)));
        })
        .WithTags(ApiTags.StudentsTag)
        .WithName("GetClassmatesPerCourse");
    }
}
