using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaRaddarStudios.Application.Features.Products.V1.Commands;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Tags;

namespace PruebaRaddarStudios.Application.Features.Products.Endpoints;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiProductsTag}", async (
            [FromBody] CreateProductRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new CreateProductCommand(request));
            return Results.Ok(result);
        })
        .RequireAuthorization()
        .WithTags(ApiTags.ApiProductsTag);
    }
}