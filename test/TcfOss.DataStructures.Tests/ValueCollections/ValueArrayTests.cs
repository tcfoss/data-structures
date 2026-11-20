using System.Collections;
using TcfOss.DataStructures.ValueCollections;

namespace TcfOss.DataStructures.Tests.ValueCollections;

public class ValueArrayTests
{
    [Fact]
    public void CreateFromArray()
    {
        int[] sourceArray = [1, 2, 3, 4, 5];
        var arr = new ValueArray<int>(sourceArray);

        Assert.Equal(5, arr.Count);
        Assert.Equal(sourceArray, arr);
#pragma warning disable xUnit2005 // Do not use identity check on value type
        Assert.NotSame(sourceArray, arr);
#pragma warning restore xUnit2005

        var arr2 = new ValueArray<int>([1, 2, 3, 4, 5]);
        Assert.Equal(arr, arr2);
        Assert.Equal(arr, (object)arr2);
        Assert.True(arr.Equals(arr2));
        Assert.True(arr.Equals((object)arr2));
        Assert.True(arr == arr2);
#pragma warning disable xUnit2005 // Do not use identity check on value type
        Assert.NotSame(arr, arr2);
#pragma warning restore xUnit2005 // Do not use identity check on value type

        Assert.Equal(arr.GetHashCode(), arr2.GetHashCode());

        Assert.Equal(arr, (object)arr2);

        for (int i = 0; i < arr.Count; i++)
        {
            Assert.Equal(i + 1, arr[i]);
        }
    }

    [Fact]
    public void CreateFromArrayNotEqual()
    {
        var arr = new ValueArray<int>([1, 2, 3, 4, 5]);
        var arr2 = new ValueArray<int>([1, 2, 3, 4, 6]);

        Assert.NotEqual(arr, arr2);
        Assert.True(arr != arr2);

        Assert.Equal(5, arr2.Count);
        for (int i = 0; i < arr2.Count; i++)
        {
            if (i < arr2.Count - 1)
            {
                Assert.Equal(arr[i], arr2[i]);
            }
            else
            {
                Assert.NotEqual(arr[i], arr2[i]);
            }
        }
    }

    [Fact]
    public void CreateFromList()
    {
        var sourceList = new List<int> { 1, 2, 3, 4, 5 };
        var arr = new ValueArray<int>(sourceList);

        Assert.Equal(5, arr.Count);
        Assert.Equal(sourceList, arr);


        for (int i = 0; i < arr.Count; i++)
        {
            Assert.Equal(i + 1, arr[i]);
        }
    }

    [Fact]
    public void Enumerate()
    {
        int[] sourceArray = [1, 2, 3, 4, 5];
        var arr = new ValueArray<int>(sourceArray);

        int index = 0;
        foreach (int item in arr)
        {
            Assert.Equal(sourceArray[index], item);
            index++;
        }

        index = 0;
        foreach (object? item in (IEnumerable)arr)
        {
            Assert.Equal(sourceArray[index], item);
            index++;
        }
    }

    [Fact]
    public void EnumerateNull()
    {
        var arr = new ValueArray<int>(null!);

        int index = 0;
        foreach (int _ in arr)
        {
            index++;
        }

        Assert.Equal(0, index);

        index = 0;
        foreach (object? _ in (IEnumerable)arr)
        {
            index++;
        }

        Assert.Equal(0, index);
    }

    [Fact]
    public void IndexNullArrayThrows()
    {
        var arr = new ValueArray<int>(null!);

        Assert.Equal(0, arr.Count);

        Assert.Equal(0, arr.GetHashCode());

        Assert.Throws<IndexOutOfRangeException>(() => _ = arr[0]);
    }

    [Fact]
    public void DefaultAndEmptyAreEqual()
    {
        ValueArray<int> defaultArray = default;
        ValueArray<int> emptyArray1 = [];
        ValueArray<int> emptyArray2 = new([]);

        Assert.Equal(defaultArray, emptyArray1);
        Assert.Equal(defaultArray.GetHashCode(), emptyArray1.GetHashCode());
        Assert.Equal(defaultArray, emptyArray2);
        Assert.Equal(defaultArray.GetHashCode(), emptyArray2.GetHashCode());

        Assert.True(defaultArray == emptyArray1);
        Assert.True(defaultArray == emptyArray2);

        HashSet<ValueArray<int>> set =
        [
            defaultArray,
            emptyArray1,
            emptyArray2
        ];
        Assert.Single(set);
    }

    [Fact]
    public void SourceNullAndEmptyAreEqual()
    {
        ValueArray<int> nullArray = new(null);
        ValueArray<int> nullIEnumerableArray = new((IEnumerable<int>?)null);
        ValueArray<int> emptyArray = new([]);

        Assert.Equal(nullArray, emptyArray);
        Assert.Equal(nullArray.GetHashCode(), emptyArray.GetHashCode());
        Assert.True(nullArray == emptyArray);

        Assert.Equal(nullIEnumerableArray, emptyArray);
        Assert.Equal(nullIEnumerableArray.GetHashCode(), emptyArray.GetHashCode());
        Assert.True(nullIEnumerableArray == emptyArray);
    }

    [Fact]
    public void EqualsNullOrDifferentTypeReturnsFalse()
    {
        var arr = new ValueArray<int>([1, 2, 3]);
        Assert.False(arr.Equals(null));
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(arr.Equals("string"));
    }

    [Fact]
    public void NullableNullAndEmptyAreDifferent()
    {
        ValueArray<int>? nullableNull = null;
        ValueArray<int>? nullableEmpty = [];

        Assert.NotEqual(nullableNull, nullableEmpty);
        Assert.False(nullableNull == nullableEmpty);
        Assert.NotEqual(nullableEmpty.GetHashCode(), nullableNull?.GetHashCode());

        HashSet<ValueArray<int>?> set =
        [
            nullableNull,
            nullableEmpty
        ];
        Assert.Equal(2, set.Count);
    }

    [Fact]
    public void ArrayIsImmutable()
    {
        int[] sourceArray = [1, 2, 3];
        var arr = new ValueArray<int>(sourceArray);

        sourceArray[0] = 42;
        Assert.Equal(1, arr[0]);
    }
}
