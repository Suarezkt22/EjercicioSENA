using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Repositories;

public class CourseRepository(DbReadContext _readContext) : ICourseRepository
{
    public async Task<List<Course>> GetByIds(List<int> ids, CancellationToken cancellationToken)
    {
        return await _readContext.Courses.Include(x => x.Teacher)
            .Where(c => ids.Contains(c.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Course>> GetByProgram(DomainProgram program, CancellationToken cancellationToken)
    {
        return await _readContext.Courses
            .Include(c => c.Teacher)
            .Where(c => c.Programs.Any(p => p.Id == program.Id))
            .ToListAsync(cancellationToken);
    }
}
