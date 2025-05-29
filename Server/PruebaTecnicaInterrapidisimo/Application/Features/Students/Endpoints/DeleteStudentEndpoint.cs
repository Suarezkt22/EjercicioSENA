using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class DeleteStudentEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete($"{ApiTags.ApiBaseRoute}/{ApiTags.StudentsTag}/delete", async (
            [FromQuery] int StudentId,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(new DeleteStudentCommand(StudentId)));
        })
        .WithTags(ApiTags.StudentsTag)
        .WithName("DeleteStudent");
    }
}
