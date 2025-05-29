using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Repositories;

public class ProgramRepository(DbReadContext _readContext) : IProgramRepository
{
    public async Task<List<DomainProgram>> GetAll(CancellationToken cancellationToken)
    {
        return await _readContext.Programs.ToListAsync(cancellationToken);
    }

    public async Task<DomainProgram?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _readContext.Programs.Include(x => x.Courses)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
