namespace CmsApi.Server.Domain.Common;

public abstract class ValueObject<T> where T : notnull, IEquatable<T>
{
    public T Value { get; }

    protected ValueObject(T value) => Value = value;

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        return ((ValueObject<T>)obj).Value.Equals(Value);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
    => Equals(left, right);

    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
        => !Equals(left, right);

    public static implicit operator T(ValueObject<T> vo) => vo.Value;

    public override string ToString() => Value.ToString()!;
}
