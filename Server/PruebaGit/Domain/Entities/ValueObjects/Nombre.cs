using GitEjercicioSENA.Common.Abstract;
using GitEjercicioSENA.Common.Exceptions;

namespace GitEjercicioSENA.Domain.Entities.ValueObjects;

public class Nombre : ValueObject<string>
{
    private const int MAX_CHARACTERS = 50;

    public Nombre(string value) : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new GeneralException("El nombre no puede estar vacío.");

        if (value.Length > MAX_CHARACTERS)
            throw new GeneralException($"El nombre no puede exceder los {MAX_CHARACTERS} caracteres.");
    }
}