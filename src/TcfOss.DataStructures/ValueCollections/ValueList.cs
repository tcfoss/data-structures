using System.Diagnostics;

namespace TcfOss.DataStructures.ValueCollections;

/// <summary>
/// A list-like entity implementing value-equality.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ValueList<T> : List<T>, ICloneable, IEquatable<ValueList<T>>
    where T : IEquatable<T>?
{
    [DebuggerStepThrough]
    public ValueList()
    { }

    [DebuggerStepThrough]
    public ValueList(IEnumerable<T> sequence) : base(sequence)
    { }

    [DebuggerStepThrough]
    public ValueList(int capacity) : base(capacity)
    { }

    public override string ToString()
    {
        return $"[{string.Join(", ", this)}]";
    }

    private bool Equals(ValueList<T>? other)
    {
        return other != null && this.SequenceEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueList<T>);
    }

    bool IEquatable<ValueList<T>>.Equals(ValueList<T>? other) => Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (T? item in this)
        {
            hash.Add(item);
        }
        return hash.ToHashCode();
    }

    public ValueList<T> Clone()
    {
        var clone = new ValueList<T>();
        foreach (T? item in this)
        {
            T? newItem = item switch
            {
                ICloneable cloneable => (T)cloneable.Clone(),
                _ => item
            };
            clone.Add(newItem);
        }
        return clone;
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    public static implicit operator T[](ValueList<T> list)
    {
        return [.. list];
    }

    public static implicit operator ValueList<T>(T[] array)
    {
        return [.. array];
    }
}
