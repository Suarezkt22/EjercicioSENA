using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Queries;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class GetAllProgramsEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ApiTags.ApiBaseRoute}/{ApiTags.ProgramsTag}/all", async (
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(new GetAllProgramsQuery()));
        })
        .WithTags(ApiTags.ProgramsTag)
        .WithName("GetAllPrograms");
    }
}
