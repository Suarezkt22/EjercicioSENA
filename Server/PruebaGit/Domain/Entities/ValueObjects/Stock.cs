using PruebaRaddarStudios.Common.Abstract;
using PruebaRaddarStudios.Common.Exceptions;

namespace PruebaRaddarStudios.Domain.Entities.ValueObjects;

public class Stock : ValueObject<int>
{
    public Stock(int value) : base(value)
    {
        if (value <= 0)
            throw new GeneralException("El stock no puede ser negativo o 0.");
    }
}