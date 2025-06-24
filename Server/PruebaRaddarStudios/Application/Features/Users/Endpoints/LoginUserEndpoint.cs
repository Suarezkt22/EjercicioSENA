using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaRaddarStudios.Application.Features.Products.V1.Commands;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Application.Features.Products.V1.Queries;
using PruebaRaddarStudios.Application.Features.Users.V1.Commands;
using PruebaRaddarStudios.Application.Features.Users.V1.DTOs;
using PruebaRaddarStudios.Common.Tags;

namespace PruebaRaddarStudios.Application.Features.Users.Endpoints;

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