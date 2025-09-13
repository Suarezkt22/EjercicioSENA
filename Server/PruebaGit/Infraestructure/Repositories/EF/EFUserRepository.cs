using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;
using GitEjercicioSENA.Infraestructure.Persistence;

namespace GitEjercicioSENA.Infraestructure.Repositories.EF;

public class EFUserRepository(DbWriteContext _writeContext, DbReadContext _readContext) : IUserRepository
{
    public Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        _writeContext.Users.Add(user);
        return Task.CompletedTask;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _readContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
