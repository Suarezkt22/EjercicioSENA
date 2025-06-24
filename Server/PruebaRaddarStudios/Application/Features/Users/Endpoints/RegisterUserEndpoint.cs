using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaRaddarStudios.Application.Features.Products.V1.Commands;
using PruebaRaddarStudios.Application.Features.Users.V1.Commands;
using PruebaRaddarStudios.Application.Features.Users.V1.DTOs;
using PruebaRaddarStudios.Common.Tags;

namespace PruebaRaddarStudios.Application.Features.Users.Endpoints;

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