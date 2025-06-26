using Hasher = BCrypt.Net;
using PruebaRaddarStudios.Domain.Contracts;

namespace PruebaRaddarStudios.Infraestructure.Services;

public class PasswordService : IPasswordService
{
    public Task<string> SecureAsync(string inputPassword, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var hashedPassword = Hasher.BCrypt.HashPassword(inputPassword);

        return Task.FromResult(hashedPassword);
    }

    public Task<bool> VerifyAsync(string savedPassword, string inputPassword, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var isValid = Hasher.BCrypt.Verify(inputPassword, savedPassword);

        return Task.FromResult(isValid);
    }
}
