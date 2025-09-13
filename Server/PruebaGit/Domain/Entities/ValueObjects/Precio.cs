using GitEjercicioSENA.Common.Abstract;
using GitEjercicioSENA.Common.Exceptions;

namespace GitEjercicioSENA.Domain.Entities.ValueObjects;

public class Precio : ValueObject<decimal>
{
    public Precio(decimal value) : base(value)
    {
        if (value <= 0)
            throw new GeneralException("El precio no puede ser negativo o 0.");
    }
}