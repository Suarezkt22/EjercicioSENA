using MediatR;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Exceptions;
using GitEjercicioSENA.Common.Wrappers;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Application.Features.Products.V1.Commands.Handlers;

public class DeleteProductCommandHandler(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteProductCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await FindProductAsync(command.ProductId, cancellationToken);

        await RemoveProductAsync(product, cancellationToken);

        return new Response<string>($"El producto {product.Nombre} ha sido eliminado correctamente.");
    }

    private async Task<Product> FindProductAsync(int productId, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(productId, cancellationToken)
               ?? throw new NotFoundException($"El producto con ID {productId} no existe.");
    }

    private async Task RemoveProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
