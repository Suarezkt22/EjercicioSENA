using MediatR;
using PruebaRaddarStudios.Application.Features.Products.V1.DTOs;
using PruebaRaddarStudios.Common.Wrappers;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Application.Features.Products.V1.Queries;

public record struct GetByIdProductQuery(int ProductId) : IRequest<Response<GetProductResponse>>;