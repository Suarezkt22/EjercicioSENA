using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class ApplyProgramEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch($"{ApiTags.ApiBaseRoute}/{ApiTags.StudentsTag}/apply-program", async (
            [FromBody] ApplyProgramCommand command,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(command));
        })
        .WithTags(ApiTags.StudentsTag)
        .WithName("ApplyProgram");
    }
}
