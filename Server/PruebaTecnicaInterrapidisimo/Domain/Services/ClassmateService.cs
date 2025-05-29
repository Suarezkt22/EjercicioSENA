using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates.ValueObjects;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PruebaTecnicaInterrapidisimo.Domain.Services;

public static class ClassmatesService
{
    /// <summary>
    /// Obtiene la información de los compañeros de clase del estudiante para un curso específico.
    /// </summary>
    /// <param name="selectedCourses">Lista de cursos en los que el estudiante está inscrito.</param>
    /// <param name="courseId">ID del curso a consultar.</param>
    /// <returns>Información de compañeros en el curso.</returns>
    /// <exception cref="InvalidOperationException">Si el estudiante no está inscrito en el curso.</exception>
    public static ClassmatesInfo GetClassmates(Student currentStudent, int courseId)
    {
        var courseSelected = currentStudent.GetRegisteredCourse(courseId);

        var classmates = courseSelected.Students.Select(student => student.Name).ToList();

        classmates.Remove(currentStudent.Name);

        return new ClassmatesInfo
        {
            CourseId = courseId,
            CourseName = courseSelected.Name,
            ClassmatesNames = classmates
        };
    }
}

