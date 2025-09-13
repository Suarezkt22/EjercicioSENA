using MediatR;
using GitEjercicioSENA.Common.Wrappers;

namespace GitEjercicioSENA.Application.Features.Products.V1.Commands;

public record struct DeleteProductCommand(int ProductId) : IRequest<Response<string>>;