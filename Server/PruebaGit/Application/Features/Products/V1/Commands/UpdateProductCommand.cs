using MediatR;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Wrappers;

namespace GitEjercicioSENA.Application.Features.Products.V1.Commands;

public record struct UpdateProductCommand(int ProductId, UpdateProductRequest Request) : IRequest<Response<string>>;