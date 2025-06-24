using MediatR;
using PruebaRaddarStudios.Common.Wrappers;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Commands;

public record struct DeleteProductCommand(int ProductId) : IRequest<Response<string>>;