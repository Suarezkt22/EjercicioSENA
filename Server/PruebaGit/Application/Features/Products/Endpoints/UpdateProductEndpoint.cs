using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GitEjercicioSENA.Application.Features.Products.V1.Commands;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Tags;

namespace GitEjercicioSENA.Application.Features.Products.Endpoints;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiProductsTag}/{{productId}}", async (
            [FromRoute] int productId,
            [FromBody] UpdateProductRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new UpdateProductCommand(productId, request));
            return Results.Ok(result);
        })
        //.RequireAuthorization()
        .WithTags(ApiTags.ApiProductsTag);
    }
}

