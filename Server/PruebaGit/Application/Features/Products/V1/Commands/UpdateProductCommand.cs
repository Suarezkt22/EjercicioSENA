using MediatR;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Wrappers;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Commands;

public record struct UpdateProductCommand(int ProductId, UpdateProductRequest Request) : IRequest<Response<string>>;