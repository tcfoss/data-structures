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
    public void ExtractArray()
    {
        int[] sourceArray = [1, 2, 3, 4, 5];
        var arr = new ValueArray<int>(sourceArray);

        int[]? extractedArray = arr.AsArray();

        Assert.Equal(sourceArray, extractedArray);
        Assert.Same(sourceArray, extractedArray);

        Assert.Equal(arr, extractedArray);
#pragma warning disable xUnit2005 // Do not use identity check on value type
        Assert.NotSame(arr, extractedArray);
#pragma warning restore xUnit2005 // Do not use identity check on value type

        foreach ((int first, int second) in arr.Zip(sourceArray))
        {
            Assert.Equal(first, second);
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
        foreach (int item in arr)
        {
            index++;
        }

        Assert.Equal(0, index);

        index = 0;
        foreach (object? item in (IEnumerable)arr)
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
    public void EqualsNullOrDifferentTypeReturnsFalse()
    {
        var arr = new ValueArray<int>([1, 2, 3]);
        Assert.False(arr.Equals(null));
        Assert.False(arr.Equals("string"));
    }
}
