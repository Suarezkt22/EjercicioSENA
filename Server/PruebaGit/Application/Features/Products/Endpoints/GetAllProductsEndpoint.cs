using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaRaddarStudios.Application.Features.Products.V1.Commands;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Application.Features.Products.V1.Queries;
using PruebaRaddarStudios.Common.Tags;

namespace PruebaRaddarStudios.Application.Features.Products.Endpoints;

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ApiTags.ApiBaseRoute}/{ApiTags.ApiProductsTag}", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllProductsQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization()
        .WithTags(ApiTags.ApiProductsTag);
    }
}

