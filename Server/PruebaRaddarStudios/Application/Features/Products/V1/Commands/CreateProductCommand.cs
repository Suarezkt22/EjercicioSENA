using MediatR;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Wrappers;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Commands;

public record struct CreateProductCommand(CreateProductRequest Request) : IRequest<Response<string>>;