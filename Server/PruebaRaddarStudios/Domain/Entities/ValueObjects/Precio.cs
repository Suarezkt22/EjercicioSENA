using PruebaRaddarStudios.Common.Abstract;
using PruebaRaddarStudios.Common.Exceptions;

namespace PruebaRaddarStudios.Domain.Entities.ValueObjects;

public class Precio : ValueObject<decimal>
{
    public Precio(decimal value) : base(value)
    {
        if (value <= 0)
            throw new GeneralException("El precio no puede ser negativo o 0.");
    }
}