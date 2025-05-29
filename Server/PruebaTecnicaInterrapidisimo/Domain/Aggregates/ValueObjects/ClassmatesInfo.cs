namespace PruebaTecnicaInterrapidisimo.Domain.Aggregates.ValueObjects;

public readonly record struct ClassmatesInfo(int CourseId, string CourseName, List<string> ClassmatesNames);