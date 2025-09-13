using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GitEjercicioSENA.Application.Features.Products.V1.Commands;
using GitEjercicioSENA.Application.Features.Users.V1.Commands;
using GitEjercicioSENA.Application.Features.Users.V1.DTOs;
using GitEjercicioSENA.Common.Tags;

namespace GitEjercicioSENA.Application.Features.Users.Endpoints;

public class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiUsersTag}/register", async (
            [FromBody] RegisterUserRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new RegisterUserCommand(request));
            return Results.Ok(result);
        })
        .WithTags(ApiTags.ApiUsersTag);
    }
}