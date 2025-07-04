using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaRaddarStudios.Application.Features.Products.V1.Commands;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Tags;

namespace PruebaRaddarStudios.Application.Features.Products.Endpoints;

public class DeleteeProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiProductsTag}/{{productId}}", async (
            [FromRoute] int productId,
            ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(productId));
            return Results.Ok(result);
        })
        .RequireAuthorization()
        .WithTags(ApiTags.ApiProductsTag);
    }
}

