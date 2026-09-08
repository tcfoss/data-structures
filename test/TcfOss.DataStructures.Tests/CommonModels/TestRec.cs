namespace TcfOss.DataStructures.Tests.CommonModels;

sealed class TestRec(string myVal) : IEquatable<TestRec>
{
    public string MyVal { get; } = myVal;

    public override string ToString()
    {
        return $"<{MyVal}>";
    }

    public bool Equals(TestRec? other)
    {
        return other != null && MyVal == other.MyVal;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TestRec);
    }

    public override int GetHashCode()
    {
        return MyVal.GetHashCode();
    }
}
