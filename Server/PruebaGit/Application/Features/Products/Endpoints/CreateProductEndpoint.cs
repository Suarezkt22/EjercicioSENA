using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GitEjercicioSENA.Application.Features.Products.V1.Commands;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Tags;

namespace GitEjercicioSENA.Application.Features.Products.Endpoints;

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
        //.RequireAuthorization()
        .WithTags(ApiTags.ApiProductsTag);
    }
}

