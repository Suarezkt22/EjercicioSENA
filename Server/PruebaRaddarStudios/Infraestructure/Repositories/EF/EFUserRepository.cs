using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;
using PruebaRaddarStudios.Infraestructure.Persistence;

namespace PruebaRaddarStudios.Infraestructure.Repositories.EF;

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
