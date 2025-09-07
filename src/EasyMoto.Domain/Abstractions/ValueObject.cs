namespace EasyMoto.Domain.Abstractions;

public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other) return false;
        using var a = GetEqualityComponents().GetEnumerator();
        using var b = other.GetEqualityComponents().GetEnumerator();
        while (a.MoveNext() && b.MoveNext())
        {
            if (a.Current is null ^ b.Current is null) return false;
            if (a.Current is not null && !a.Current.Equals(b.Current)) return false;
        }
        return !a.MoveNext() && !b.MoveNext();
    }

    public override int GetHashCode() => GetEqualityComponents().Aggregate(1, HashCode.Combine);
}
