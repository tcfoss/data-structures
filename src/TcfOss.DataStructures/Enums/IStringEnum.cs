using System.Diagnostics.CodeAnalysis;
using TcfOss.DataStructures.ValueCollections;

namespace TcfOss.DataStructures.Enums;

#pragma warning disable CA1711 // Suffix 'Enum' is by design

public interface IStringEnum<T> : IEquatable<T>
{
    static abstract ValueArray<string> AllowedValues { get; }
    static abstract T Parse(string text);
    static abstract bool TryParse(string text, [NotNullWhen(true)] out T? value);
}
