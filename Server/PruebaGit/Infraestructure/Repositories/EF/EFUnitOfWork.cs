using Microsoft.EntityFrameworkCore;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Infraestructure.Persistence;
using System;
using System.Threading.Tasks;

namespace GitEjercicioSENA.Infraestructure.Repositories.EF;

public class EFUnitOfWork(DbWriteContext _context) : IUnitOfWork
{
    private bool _disposed;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }
}
