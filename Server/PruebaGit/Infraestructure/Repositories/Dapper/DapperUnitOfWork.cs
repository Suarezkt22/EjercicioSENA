using System.Data;
using GitEjercicioSENA.Domain.Contracts;

namespace GitEjercicioSENA.Infraestructure.Repositories.Dapper;

public class DapperUnitOfWork() : IUnitOfWork
{
    private bool _disposed;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        await Task.CompletedTask;
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
                return;
            }
            _disposed = true;
        }
    }
}
