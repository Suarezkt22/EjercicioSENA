using PruebaRaddarStudios.Common.Abstract;
using PruebaRaddarStudios.Common.Exceptions;

namespace PruebaRaddarStudios.Domain.Entities.ValueObjects;

public class Descripcion : ValueObject<string>
{
    private const int MAX_CHARACTERS = 100;

    public Descripcion(string value) : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new GeneralException("La descripcion no puede estar vacía.");

        if (value.Length > MAX_CHARACTERS)
            throw new GeneralException($"La descripcion no puede exceder los {MAX_CHARACTERS} caracteres.");
    }
}