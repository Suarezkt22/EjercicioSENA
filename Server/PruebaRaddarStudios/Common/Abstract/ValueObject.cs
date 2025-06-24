namespace PruebaRaddarStudios.Common.Abstract;

public abstract class ValueObject<T> where T : notnull
{
    public T Value { get; }

    protected ValueObject(T value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value), "El valor no puede ser nulo.");

        Value = value;
    }

    public override string ToString() => Value.ToString() ?? string.Empty;

    public override bool Equals(object? obj) =>
        obj is ValueObject<T> other && EqualityComparer<T>.Default.Equals(Value, other.Value);

    public override int GetHashCode() => Value?.GetHashCode() ?? 0;

    // Operadores opcionales para facilitar el uso
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) =>
        Equals(left, right);

    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) =>
        !Equals(left, right);
}
