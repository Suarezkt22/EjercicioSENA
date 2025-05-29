using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Domain.Contracts;

public interface IProgramRepository
{
    Task<DomainProgram?> GetById(int id, CancellationToken cancellationToken);

    Task<List<DomainProgram>> GetAll(CancellationToken cancellationToken);
}
