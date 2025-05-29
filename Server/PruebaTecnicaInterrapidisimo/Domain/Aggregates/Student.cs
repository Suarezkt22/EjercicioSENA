using PruebaTecnicaInterrapidisimo.Common.Abstract;
using PruebaTecnicaInterrapidisimo.Common.Exceptions;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using System.Collections.ObjectModel;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Domain.Aggregates;

public class Student : TAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public DomainProgram? Program { get; private set; }

    private readonly List<Course> _enrolledCourses = [];
    public IReadOnlyCollection<Course> EnrolledCourses => new ReadOnlyCollection<Course>(_enrolledCourses);

    private const int MAX_COURSES = 3;

    // Constructor para EF Core
    protected Student() { }

    private Student(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del estudiante no puede estar vacío", nameof(name));

        Name = name.Trim();
    }

    public static Student Create(string name) => new(name);

    public void ApplyProgram(DomainProgram program)
    {
        if (Program != null)
            throw new GeneralException($"El estudiante {Name} ya está inscrito en el programa {Program.Name}.");

        Program = program;
    }

    public void EnrollCourses(List<Course> coursesToRegister)
    {
        ValidateEnrollCourses();
        ValidateProgramRequirements();
        ValidateCourseCount(coursesToRegister);
        ValidateCoursesAgainstProgram(coursesToRegister);
        ValidateTeachersUniqueness(coursesToRegister);


        UpdateStudentCourses(coursesToRegister);
    }

    public Course GetRegisteredCourse(int courseId)
    {
        return EnrolledCourses.FirstOrDefault(c => c.Id == courseId)
            ?? throw new NotFoundException($"El estudiante no está inscrito en el curso con ID {courseId}");
    }

    public void ValidateEnrollCourses()
    {
        var cantEnroll = _enrolledCourses.Count > 0;

        if (cantEnroll)
        {
            throw new GeneralException($"El estudiante {Name} ya tiene materias inscritas. No puede modificar su matrícula.");
        }
    }

    private void ValidateProgramRequirements()
    {
        if (Program == null)
            throw new GeneralException("Debe asignar un programa académico antes de matricular cursos.");

        if (Program.Courses == null || Program.Courses.Count == 0)
            throw new GeneralException("El programa asignado no tiene cursos disponibles.");
    }

    private void ValidateCoursesAgainstProgram(List<Course> courses)
    {
        var programCourseIds = new HashSet<int>(Program!.Courses.Select(c => c.Id));
        var invalidCourses = courses.Where(c => !programCourseIds.Contains(c.Id)).ToList();

        if (invalidCourses.Count != 0)
        {
            var invalidNames = string.Join(", ", invalidCourses.Select(c => c.Name));
            throw new GeneralException($"Cursos no pertenecientes al programa: {invalidNames}");
        }
    }

    private static void ValidateTeachersUniqueness(List<Course> courses)
    {
        var teacherGroups = courses
            .Where(c => c.Teacher != null)
            .GroupBy(c => c.Teacher!.Id)
            .Where(g => g.Count() > 1)
            .ToList();

        if (teacherGroups.Count != 0)
        {
            var teacherInfo = teacherGroups.Select(g =>
            {
                var teacher = g.First().Teacher!;
                var courseDetails = string.Join(", ", g.Select(c => $"{c.Name} (ID: {c.Id})"));
                return $"{teacher.Name} en materias: {courseDetails}";
            });

            throw new GeneralException($"Profesores duplicados: {string.Join(" | ", teacherInfo)}");
        }
    }

    private static void ValidateCourseCount(List<Course> courses)
    {
        if (courses.Count == 0)
            throw new GeneralException($"Debe seleccionar al menos 1 curso.");

        if (courses.Count > MAX_COURSES)
            throw new GeneralException($"No puede inscribir más de {MAX_COURSES} cursos.");
    }

    private void UpdateStudentCourses(List<Course> coursesToRegister)
    {
        var programCourses = Program!.Courses.ToDictionary(c => c.Id);

        foreach (var course in coursesToRegister)
        {
            if (programCourses.TryGetValue(course.Id, out var programCourse))
            {
                _enrolledCourses.Add(programCourse);
            }
        }

        if (_enrolledCourses.Count != coursesToRegister.Count)
        {
            throw new GeneralException(
                $"Inconsistencia al asignar cursos. Esperados: {coursesToRegister.Count}, Asignados: {_enrolledCourses.Count}");
        }
    }
}