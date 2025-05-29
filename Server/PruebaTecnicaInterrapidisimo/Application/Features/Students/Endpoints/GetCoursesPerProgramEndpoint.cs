using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class GetCoursesPerProgramEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ApiTags.ApiBaseRoute}/{ApiTags.CoursesTag}/per-program", async (
            [FromQuery] int programId,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(new GetCoursesPerProgramQuery(programId)));
        })
        .WithTags(ApiTags.CoursesTag)
        .WithName("GetCoursesPerProgram");
    }
}
