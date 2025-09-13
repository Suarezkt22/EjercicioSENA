using MediatR;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Exceptions;
using GitEjercicioSENA.Common.Wrappers;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Application.Features.Products.V1.Commands.Handlers;

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

        if (string.IsNullOrWhiteSpace(nombre))
            throw new GeneralException("El nombre del producto es obligatorio.");

        if (precio <= 0)
            throw new GeneralException("El precio debe ser mayor a 0.");

        if (stock < 0)
            throw new GeneralException("El stock no puede ser negativo.");

        return Product.Create(nombre, descripcion, precio, stock);
    }

    private async Task PersistProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.CreateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
