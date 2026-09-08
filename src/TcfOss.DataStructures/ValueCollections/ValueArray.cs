using System.Collections;

namespace TcfOss.DataStructures.ValueCollections;

#pragma warning disable CA2201 // System.IndexOutOfRangeException is by design

/// <summary>
/// An immutable, equatable array. This is equivalent to <see cref="Array"/> but with value equality support.
/// </summary>
/// <typeparam name="T">The type of values in the array.</typeparam>
public readonly struct ValueArray<T>(T[] array) : IEquatable<ValueArray<T>>, IEnumerable<T>
    where T : IEquatable<T>
{
    public ValueArray(IEnumerable<T> items) : this([.. items])
    { }

    private readonly T[]? _array = array;

    /// <summary>
    /// Gets the length of the array, or 0 if the array is null
    /// </summary>
    public int Count => _array?.Length ?? 0;

    /// <summary>
    /// Checks whether two <see cref="ValueArray{T}"/> values are the same.
    /// </summary>
    /// <param name="left">The first <see cref="ValueArray{T}"/> value.</param>
    /// <param name="right">The second <see cref="ValueArray{T}"/> value.</param>
    /// <returns>Whether <paramref name="left"/> and <paramref name="right"/> are equal.</returns>
    public static bool operator ==(ValueArray<T> left, ValueArray<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks whether two <see cref="ValueArray{T}"/> values are not the same.
    /// </summary>
    /// <param name="left">The first <see cref="ValueArray{T}"/> value.</param>
    /// <param name="right">The second <see cref="ValueArray{T}"/> value.</param>
    /// <returns>Whether <paramref name="left"/> and <paramref name="right"/> are not equal.</returns>
    public static bool operator !=(ValueArray<T> left, ValueArray<T> right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc/>
    public bool Equals(ValueArray<T> array)
    {
        return AsSpan().SequenceEqual(array.AsSpan());
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ValueArray<T> array && Equals(array);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (_array is not T[] array)
        {
            return 0;
        }

        HashCode hashCode = default;

        foreach (T item in array)
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{T}"/> wrapping the current items.
    /// </summary>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> wrapping the current items.</returns>
    public ReadOnlySpan<T> AsSpan()
    {
        return _array.AsSpan();
    }

    /// <summary>
    /// Returns the underlying wrapped array.
    /// </summary>
    /// <returns>Returns the underlying array.</returns>
    public T[]? AsArray()
    {
        return _array;
    }

    public T this[int index]
    {
        get
        {
            if (_array == null)
            {
                throw new IndexOutOfRangeException("The array is empty.");
            }

            return _array[index];
        }
    }

    /// <inheritdoc/>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    }
}
