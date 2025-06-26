using MediatR;
using PruebaRaddarStudios.Application.Features.Users.V1.DTOs;
using PruebaRaddarStudios.Common.Wrappers;

namespace PruebaRaddarStudios.Application.Features.Users.V1.Commands;

public record struct LoginUserCommand(LoginUserRequest Request) : IRequest<Response<LoginUserResponse>>;