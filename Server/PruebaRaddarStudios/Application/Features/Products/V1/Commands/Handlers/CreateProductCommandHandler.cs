using MediatR;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Wrappers;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Commands.Handlers;

public class CreateProductCommandHandler(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<CreateProductCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productToCreate = MapToDomain(command.Request);

        await PersistProductAsync(productToCreate, cancellationToken);

        return new Response<string>($"El producto {productToCreate.Nombre} ha sido creado correctamente.");
    }

    private static Product MapToDomain(CreateProductRequest productRequest)
    {
        var (nombre, descripcion, precio, stock) = productRequest;

        return Product.Create(nombre, descripcion, precio, stock);
    }

    private async Task PersistProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.CreateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
