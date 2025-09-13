using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GitEjercicioSENA.Application.Features.Products.V1.Commands;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Application.Features.Products.V1.Queries;
using GitEjercicioSENA.Common.Tags;

namespace GitEjercicioSENA.Application.Features.Products.Endpoints;

public class GetByIdProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiProductsTag}/{{productId}}", async (
            [FromRoute] int productId,
            ISender sender) =>
        {
            var result = await sender.Send(new GetByIdProductQuery(productId));
            return Results.Ok(result);
        })
        //.RequireAuthorization()
        .WithTags(ApiTags.ApiProductsTag);
    }
}

