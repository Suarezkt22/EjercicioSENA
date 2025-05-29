using System.Collections.ObjectModel;
using PruebaTecnicaInterrapidisimo.Common.Abstract;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;

namespace PruebaTecnicaInterrapidisimo.Domain.Entities;

public class Program : TEntity
{
    public string Name { get; private set; } = string.Empty;
    public int Credits { get; private set; }
    private readonly List<Course> _courses  = [];
    public IReadOnlyCollection<Course> Courses => new ReadOnlyCollection<Course>(_courses);
    private readonly List<Student> _students = [];
    public IReadOnlyCollection<Student> Students => new ReadOnlyCollection<Student>(_students);

    // Constructor para el mapeo de EF
    protected Program() { } 
}

