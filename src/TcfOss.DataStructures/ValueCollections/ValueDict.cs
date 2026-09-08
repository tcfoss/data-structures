namespace TcfOss.DataStructures.ValueCollections;

public sealed class ValueDict<TKey, TVal> : Dictionary<TKey, TVal>, ICloneable, IEquatable<ValueDict<TKey, TVal>>
    where TKey : notnull, IEquatable<TKey>
    where TVal : IEquatable<TVal>
{
    public ValueDict()
    { }

    public ValueDict(Dictionary<TKey, TVal> dict) : base(dict)
    { }

    public ValueDict(ValueDict<TKey, TVal> previous) : base(previous.Clone())
    { }

    public ValueDict(IEnumerable<KeyValuePair<TKey, TVal>> source) : base(source)
    { }

    private bool Equals(ValueDict<TKey, TVal>? other)
    {
        if (other == null)
        {
            return false;
        }

        if (Count != other.Count)
        {
            return false;
        }

        foreach (TKey key in Keys)
        {
            if (!other.TryGetValue(key, out TVal? otherValue))
            {
                return false;
            }

            if (!EqualityComparer<TVal>.Default.Equals(this[key], otherValue))
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueDict<TKey, TVal>);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (KeyValuePair<TKey, TVal> kvp in this)
        {
            hash.Add(kvp.Key);
            hash.Add(kvp.Value);
        }
        return hash.ToHashCode();
    }

    public ValueDict<TKey, TVal> Clone()
    {
        var clone = new ValueDict<TKey, TVal>();
        foreach (KeyValuePair<TKey, TVal> kvp in this)
        {
            TKey newKey = kvp.Key switch
            {
                ICloneable cloneableKey => (TKey)cloneableKey.Clone(),
                _ => kvp.Key
            };
            TVal newVal = kvp.Value switch
            {
                ICloneable cloneableVal => (TVal)cloneableVal.Clone(),
                _ => kvp.Value
            };
            clone.Add(newKey, newVal);
        }
        return clone;
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    bool IEquatable<ValueDict<TKey, TVal>>.Equals(ValueDict<TKey, TVal>? other) => Equals(other);
}
