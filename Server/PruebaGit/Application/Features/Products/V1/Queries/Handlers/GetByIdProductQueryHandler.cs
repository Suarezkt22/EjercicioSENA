using MediatR;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Exceptions;
using GitEjercicioSENA.Common.Wrappers;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Application.Features.Products.V1.Queries.Handlers;

public class GetByIdProductQueryHandler(
    IProductRepository _productRepository
) : IRequestHandler<GetByIdProductQuery, Response<GetProductResponse>>
{
    public async Task<Response<GetProductResponse>> Handle(GetByIdProductQuery query, CancellationToken cancellationToken)
    {
        var product = await FindProductAsync(query.ProductId, cancellationToken);

        var response = BuildResponse(product);

        return new Response<GetProductResponse>(response);
    }

    private static GetProductResponse BuildResponse(Product product)
    {
        return new GetProductResponse(
            product.Id,
            product.Nombre.Value,
            product.Descripcion.Value,
            product.Precio.Value,
            product.Stock.Value,
            product.FechaCreacion
        ); 
    }

    private async Task<Product> FindProductAsync(int productId, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(productId, cancellationToken)
               ?? throw new NotFoundException($"El producto con ID {productId} no existe.");
    }
}
