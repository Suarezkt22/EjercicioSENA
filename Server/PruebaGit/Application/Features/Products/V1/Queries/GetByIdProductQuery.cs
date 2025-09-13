using MediatR;
using GitEjercicioSENA.Application.Features.Products.V1.DTOs;
using GitEjercicioSENA.Common.Wrappers;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Application.Features.Products.V1.Queries;

public record struct GetByIdProductQuery(int ProductId) : IRequest<Response<GetProductResponse>>;