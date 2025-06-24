namespace PruebaRaddarStudios.Application.Features.Products.V1.DTOs;

public record struct GetProductResponse(int Id, string Nombre, string Descripcion, decimal Precio, int Stock, DateTime FechaCreacion);