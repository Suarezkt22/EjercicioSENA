namespace PruebaRaddarStudios.Application.Features.Products.V1.DTOs;

public record struct UpdateProductRequest(string Nombre, string Descripcion, decimal Precio, int Stock);