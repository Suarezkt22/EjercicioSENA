namespace GitEjercicioSENA.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
