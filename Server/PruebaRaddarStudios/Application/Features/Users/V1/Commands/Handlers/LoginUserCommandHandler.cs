using MediatR;
using PruebaRaddarStudios.Application.Features.Users.V1.DTOs;
using PruebaRaddarStudios.Common.Exceptions;
using PruebaRaddarStudios.Common.Wrappers;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Application.Features.Users.V1.Commands.Handlers;

public class LoginUserCommandHandler(
    IUserRepository _userRepository,
    IPasswordService _passwordService,
    ITokenService _tokenService
) : IRequestHandler<LoginUserCommand, Response<LoginUserResponse>>
{
    public async Task<Response<LoginUserResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(command.Request.Email, cancellationToken);

        await VerifyPasswordAsync(user.Password, command.Request.Password, cancellationToken);

        var response = BuildResponse(user);

        return new Response<LoginUserResponse>(response);
    }

    private async Task<User> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByEmailAsync(email, cancellationToken) ?? throw new UnauthorizedException();
    }

    private async Task VerifyPasswordAsync(string savedPassword, string inputPassword, CancellationToken cancellationToken)
    {
        var valid = await _passwordService.VerifyAsync(savedPassword, inputPassword, cancellationToken);

        if (!valid)
        {
            throw new UnauthorizedException();
        }
    }

    private LoginUserResponse BuildResponse(User user)
    {
        return new LoginUserResponse
        {
            Token = _tokenService.Generate(user.RetrieveData())
        };
    }
}
