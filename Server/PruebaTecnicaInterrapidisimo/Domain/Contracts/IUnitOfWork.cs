namespace PruebaTecnicaInterrapidisimo.Domain.Contracts;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}