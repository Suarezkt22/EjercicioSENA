using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GitEjercicioSENA.Application.Features.Products.V1.Commands;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Application.Features.Products.V1.Queries;
using GitEjercicioSENA.Application.Features.Users.V1.Commands;
using GitEjercicioSENA.Application.Features.Users.V1.DTOs;
using GitEjercicioSENA.Common.Tags;

namespace GitEjercicioSENA.Application.Features.Users.Endpoints;

public class LoginUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiUsersTag}/login", async (
            [FromBody] LoginUserRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new LoginUserCommand(request));
            return Results.Ok(result);
        })
        .WithTags(ApiTags.ApiUsersTag);
    }
}