using PruebaTecnicaInterrapidisimo.Domain.Contracts;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;

public class UnitOfWork(DbWriteContext _context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}
