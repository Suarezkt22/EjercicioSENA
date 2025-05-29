using Carter;
using MediatR;
using PruebaTecnicaInterrapidisimo.Common.Tags;
using PruebaTecnicaInterrapidisimo.Application.Features.Students.V1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaInterrapidisimo.Application.Features.Students.Endpoints;

public class EnrollCoursesEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch($"{ApiTags.ApiBaseRoute}/{ApiTags.StudentsTag}/enroll-courses", async (
            [FromBody] EnrollCoursesCommand command,
            ISender sender) =>
        { 
            return Results.Ok(await sender.Send(command));
        })
        .WithTags(ApiTags.StudentsTag)
        .WithName("EnrollCourses");
    }
}
