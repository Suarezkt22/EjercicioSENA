using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Contracts;
using PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Repositories;

public class StudentRepository(DbWriteContext _writeContext, DbReadContext _readContext) : IStudentRepository
{
    public Task Create(Student student, CancellationToken cancellationToken)
    {
        _writeContext.Students.Add(student);
        return Task.CompletedTask;
    }

    public Task Delete(Student student, CancellationToken cancellationToken)
    {
        _writeContext.Students.Remove(student);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await _readContext.Students
            .AnyAsync(s => s.Name == name, cancellationToken);
    }

    public async Task<Student?> GetById(int id, CancellationToken cancellationToken)
    {
        var studentExists = await _readContext.Students
            .AnyAsync(s => s.Id == id, cancellationToken);

        if (!studentExists) return null;

        return await _writeContext.Students.Include(x => x.EnrolledCourses)
            .Include(x => x.Program)
                .ThenInclude(x => x.Courses)
                    .ThenInclude(x => x.Students)
            .Include(x => x.Program)
                .ThenInclude(x => x.Courses)
                    .ThenInclude(x => x.Teacher)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public Task Update(Student student, CancellationToken cancellationToken)
    {
        _writeContext.Entry(student.Program!).State = EntityState.Unchanged;
        _writeContext.Students.Update(student);
        return Task.CompletedTask;
    }
}
