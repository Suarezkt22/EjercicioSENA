using PruebaTecnicaInterrapidisimo.Domain.Entities;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Domain.Contracts;

public interface ICourseRepository
{
    Task<List<Course>> GetByIds(List<int> ids, CancellationToken cancellationToken);
}
