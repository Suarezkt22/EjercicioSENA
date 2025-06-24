namespace PruebaRaddarStudios.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
