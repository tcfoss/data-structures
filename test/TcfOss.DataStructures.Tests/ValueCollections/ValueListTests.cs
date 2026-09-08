using TcfOss.DataStructures.Tests.CommonModels;
using TcfOss.DataStructures.ValueCollections;

namespace TcfOss.DataStructures.Tests.ValueCollections;

public class ValueListTests
{
    [Fact]
    public void ConstructFromOtherValueList()
    {
        var list = new ValueList<int> { 1, 2, 3, 4, 5 };

        var list2 = new ValueList<int>(list);

        Assert.Equal(list, list2);
        Assert.NotSame(list, list2);

        for (int i = 0; i < list.Count; i++)
        {
            Assert.Equal(list[i], list2[i]);
        }
    }

    [Fact]
    public void ConstructFromOtherValueListNotEqual()
    {
        var list = new ValueList<int> { 1, 2, 3, 4, 5 };

        var list2 = new ValueList<int> { 1, 2, 3, 4, 6 };

        Assert.NotEqual(list, list2);
        Assert.NotSame(list, list2);

        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count - 1)
            {
                Assert.Equal(list[i], list2[i]);
            }
            else
            {
                Assert.NotEqual(list[i], list2[i]);
            }
        }
    }

    [Fact]
    public void ConstructFromRegularList()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };

        var list2 = new ValueList<int>(list);

        Assert.Equal(list, list2);
        Assert.NotSame(list, list2);

        for (int i = 0; i < list.Count; i++)
        {
            Assert.Equal(list[i], list2[i]);
        }
    }

    [Fact]
    public void ConstructFromArray()
    {
        int[] array = [1, 2, 3, 4, 5];

        var list = new ValueList<int>(array);

        Assert.Equal(array.Length, list.Count);

        for (int i = 0; i < array.Length; i++)
        {
            Assert.Equal(array[i], list[i]);
        }
    }

    [Fact]
    public void ConstructFromCapacity()
    {
        var list = new ValueList<int>(7);

        Assert.Empty(list);
        Assert.Equal(7, list.Capacity);
    }

    [Fact]
    public void ConstructFromOtherValueListReferenceTypes()
    {
        var list = new ValueList<TestRec>
        {
            new("val1"),
            new("val2"),
            new("val3"),
        };

        var list2 = new ValueList<TestRec>(list);

        Assert.Equal(list, list2);
        Assert.NotSame(list, list2);

        for (int i = 0; i < list.Count; i++)
        {
            Assert.Equal(list[i], list2[i]);
            Assert.Same(list[i], list2[i]);
        }
    }

    [Fact]
    public void ConstructFromRegularListReferenceTypes()
    {
        var list = new List<TestRec>
        {
            new("val1"),
            new("val2"),
            new("val3"),
        };

        var list2 = new ValueList<TestRec>(list);

        Assert.Equal(list, list2);
        Assert.Equal(list, (object)list2);
        Assert.NotSame(list, list2);

        for (int i = 0; i < list.Count; i++)
        {
            Assert.Equal(list[i], list2[i]);
            Assert.Same(list[i], list2[i]);
        }
    }

    [Fact]
    public void NotEqualNull()
    {
        var list = new ValueList<int> { 1, 2, 3 };

        Assert.False(list.Equals(null));
    }

    [Fact]
    public void BuildElements()
    {
        var list = new ValueList<int>
        {
            1,
            2,
            3,
            4,
            5
        };

        Assert.Equal(5, list.Count);

        for (int i = 0; i < 5; i++)
        {
            Assert.Equal(i + 1, list[i]);
        }
    }

    [Fact]
    public void ToStringWorks()
    {
        var seq = new ValueList<TestRec>
        {
            new("col1"),
            new("col2"),
            new("col3"),
            new("col4"),
        };

        string str = seq.ToString();

        Assert.Equal("[<col1>, <col2>, <col3>, <col4>]", str);
    }

    [Fact]
    public void IsHashable()
    {
        var seq1 = new ValueList<int>
        {
            1,
            2,
            3,
            4,
        };

        var seq2 = new ValueList<int>
        {
            5,
            6,
            7,
            8,
        };

        var dict = new Dictionary<ValueList<int>, string>
        {
            [seq1] = "first",
            [seq2] = "second",
        };

        Assert.Equal("first", dict[seq1]);
        Assert.Equal("second", dict[seq2]);
    }

    [Fact]
    public void CloneWorksWithNonCloneableItems()
    {
        var seq = new ValueList<TestRec>
        {
            new("col1"),
            new("col2"),
            new("col3"),
            new("col4"),
        };

        ValueList<TestRec> cloned = seq.Clone();

        Assert.Equal(seq, cloned);
        Assert.NotSame(seq, cloned);

        foreach ((TestRec? first, TestRec? second) in seq.Zip(cloned))
        {
            Assert.Equal(first, second);
            Assert.Same(first, second);
        }

        object cloned2 = (seq as ICloneable)!.Clone();

        Assert.Equal(seq, cloned2);
        Assert.NotSame(seq, cloned2);
    }

    [Fact]
    public void CloneWorksWithCloneableItems()
    {
        var seq = new ValueList<CloneableTestRec>
        {
            new("val1"),
            new("val2"),
            new("val3"),
        };

        ValueList<CloneableTestRec> cloned = seq.Clone();

        Assert.Equal(seq, cloned);
        Assert.NotSame(seq, cloned);

        for (int i = 0; i < seq.Count; i++)
        {
            Assert.Equal(seq[i], cloned[i]);
            Assert.NotSame(seq[i], cloned[i]);
        }
    }

    [Fact]
    public void ImplicitConvertToArray()
    {
        var list = new ValueList<TestRec>
        {
            new("val1"),
            new("val2"),
            new("val3"),
        };

        TestRec[] array = list;

        Assert.Equal(list.Count, array.Length);
        Assert.Equal(list, array);
    }

    [Fact]
    public void ImplicitConvertFromArray()
    {
        var array = new TestRec[]
        {
            new("val1"),
            new("val2"),
            new("val3"),
        };

        ValueList<TestRec> list = array;

        Assert.Equal(array.Length, list.Count);
        Assert.Equal(array, list);
    }
}
