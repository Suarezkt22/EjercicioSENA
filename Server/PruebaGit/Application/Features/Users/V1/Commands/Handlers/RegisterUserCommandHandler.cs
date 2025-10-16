using MediatR;
using GitEjercicioSENA.Application.Features.Users.V1.DTOs;
using GitEjercicioSENA.Common.Exceptions;
using GitEjercicioSENA.Common.Wrappers;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Application.Features.Users.V1.Commands.Handlers;

public class RegisterUserCommandHandler(
    IUserRepository _userRepository,
    IPasswordService _passwordService,
    IUnitOfWork _unitOfWork
) : IRequestHandler<RegisterUserCommand, Response<string>>
{
    public async Task<Response<string>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new GeneralException("El correo es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new GeneralException("La contraseña es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            throw new GeneralException("El correo y la contraseña son obligatorios.");

        var userToCreate = MapToDomain(command.Request);

        await EnsureEmailNotRegisteredAsync(userToCreate.Email, cancellationToken);

        await SecurePasswordAsync(userToCreate, cancellationToken);

        await PersistUserAsync(userToCreate, cancellationToken);

        return new Response<string>("El usuario se ha registrado correctamente.");
    }

    private static User MapToDomain(RegisterUserRequest userRequest)
    {
        var (email, password) = userRequest;

        return User.Create(email, password);
    }

    private async Task EnsureEmailNotRegisteredAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is not null)
        {
            throw new GeneralException($"El correo {email} ya está registrado.");
        }
    }

    private async Task SecurePasswordAsync(User userToCreate, CancellationToken cancellationToken)
    {
        var hashedPassword = await _passwordService.SecureAsync(userToCreate.Password, cancellationToken);
        userToCreate.UpdateSecuredPassword(hashedPassword);
    }

    private async Task PersistUserAsync(User user, CancellationToken cancellationToken)
    {
        await _userRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}