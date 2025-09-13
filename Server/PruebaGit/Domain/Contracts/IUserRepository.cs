using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Domain.Contracts;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task CreateAsync(User user, CancellationToken cancellationToken);

}