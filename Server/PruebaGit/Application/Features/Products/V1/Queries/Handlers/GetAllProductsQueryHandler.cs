using MediatR;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Wrappers;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Queries.Handlers;

public class GetAllProductsQueryHandler(
    IProductRepository _productRepository
) : IRequestHandler<GetAllProductsQuery, Response<List<GetProductResponse>>>
{
    public async Task<Response<List<GetProductResponse>>> Handle(GetAllProductsQuery command, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        var response = BuildResponse(products);

        return new Response<List<GetProductResponse>>(response);
    }

    private static List<GetProductResponse> BuildResponse(List<Product> products)
    {
        return products.ConvertAll(product => new GetProductResponse(
            product.Id,
            product.Nombre.Value,
            product.Descripcion.Value,
            product.Precio.Value,
            product.Stock.Value,
            product.FechaCreacion
        ));
    }
}
