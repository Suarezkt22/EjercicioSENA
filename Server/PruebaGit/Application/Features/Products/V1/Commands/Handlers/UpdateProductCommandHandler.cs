using MediatR;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Exceptions;
using PruebaRaddarStudios.Common.Wrappers;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Commands.Handlers;

public class UpdateProductCommandHandler(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<UpdateProductCommand, Response<string>>
{
    public async Task<Response<string>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await FindProductAsync(command.ProductId, cancellationToken);

        ApplyUpdates(existingProduct, command.Request);

        await PersistUpdatedProductAsync(existingProduct, cancellationToken);

        return new Response<string>($"El producto {existingProduct.Nombre} ha sido actualizado correctamente.");
    }

    private static void ApplyUpdates(Product product, UpdateProductRequest updateRequest)
    {
        var (name, description, price, stock) = updateRequest;

        if (string.IsNullOrWhiteSpace(name))
            throw new GeneralException("El nombre del producto es obligatorio.");

        if (price <= 0)
            throw new GeneralException("El precio debe ser mayor a 0.");

        if (stock < 0)
            throw new GeneralException("El stock no puede ser negativo.");

        product.Update(name, description, price, stock);
    }

    private async Task<Product> FindProductAsync(int productId, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(productId, cancellationToken)
               ?? throw new NotFoundException($"El producto con ID {productId} no existe.");
    }

    private async Task PersistUpdatedProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
