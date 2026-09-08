namespace TcfOss.DataStructures.Tests.CommonModels;

sealed class CloneableTestRec(string myVal) : ICloneable, IEquatable<CloneableTestRec>
{
    public string MyVal { get; } = myVal;

    public override string ToString()
    {
        return $"<{MyVal}>";
    }

    public object Clone()
    {
        return new CloneableTestRec(MyVal);
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    public bool Equals(CloneableTestRec? other)
    {
        return other != null && MyVal == other.MyVal;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CloneableTestRec);
    }

    public override int GetHashCode()
    {
        return MyVal.GetHashCode();
    }
}
