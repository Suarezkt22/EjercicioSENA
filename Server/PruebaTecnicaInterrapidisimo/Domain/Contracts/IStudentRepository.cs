using PruebaTecnicaInterrapidisimo.Domain.Aggregates;

namespace PruebaTecnicaInterrapidisimo.Domain.Contracts;

public interface IStudentRepository
{
    Task Create(Student student, CancellationToken cancellationToken);
    Task Update(Student student, CancellationToken cancellationToken);
    Task<Student?> GetById(int id, CancellationToken cancellationToken);
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
    Task Delete(Student student, CancellationToken cancellationToken);
}
