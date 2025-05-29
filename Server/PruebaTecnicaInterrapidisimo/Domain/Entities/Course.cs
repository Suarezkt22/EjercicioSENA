using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using PruebaTecnicaInterrapidisimo.Common.Abstract;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;

namespace PruebaTecnicaInterrapidisimo.Domain.Entities;

public class Course : TEntity
{
    public string Name { get; private set; } = string.Empty;
    public int Credits { get; } = 3;
    public Teacher? Teacher { get; private set; }
    [JsonIgnore] 
    private readonly List<Program> _programs = new List<Program>();
    [JsonIgnore] 
    public IReadOnlyCollection<Program> Programs => new ReadOnlyCollection<Program>(_programs);

    private readonly List<Student> _students = new List<Student>();
    public IReadOnlyCollection<Student> Students => new ReadOnlyCollection<Student>(_students);

    // Constructor para EF Core
    protected Course() { }
}
