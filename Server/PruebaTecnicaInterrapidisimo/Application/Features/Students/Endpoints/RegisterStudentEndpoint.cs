using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class RegisterStudentEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost($"{ApiTags.ApiBaseRoute}/{ApiTags.StudentsTag}/register", async (
            [FromBody] RegisterStudentCommand command,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(command));
        })
        .WithTags(ApiTags.StudentsTag)
        .WithName("RegisterStudent");
    }
}
