using MediatR;
using GitEjercicioSENA.Application.Features.Users.V1.DTOs;
using GitEjercicioSENA.Common.Wrappers;

namespace GitEjercicioSENA.Application.Features.Users.V1.Commands;

public record struct LoginUserCommand(LoginUserRequest Request) : IRequest<Response<LoginUserResponse>>;