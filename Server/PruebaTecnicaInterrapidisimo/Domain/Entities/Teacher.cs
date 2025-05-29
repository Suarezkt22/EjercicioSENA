using PruebaTecnicaInterrapidisimo.Common.Abstract;

namespace PruebaTecnicaInterrapidisimo.Domain.Entities;

public class Teacher : TEntity
{
    public string Name { get; private set; } = string.Empty;
    public virtual ICollection<Course> Courses { get; private set; } = [];

    // Constructor para el mapeo de EF
    protected Teacher() { } 
}
