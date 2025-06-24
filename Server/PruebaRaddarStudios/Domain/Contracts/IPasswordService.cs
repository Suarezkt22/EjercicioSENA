namespace PruebaRaddarStudios.Domain.Contracts;

public interface IPasswordService
{
    Task<string> SecureAsync(string inputPassword, CancellationToken cancellationToken);

    Task<bool> VerifyAsync(string savedPassword, string inputPassword, CancellationToken cancellationToken);
}