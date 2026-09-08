using TcfOss.DataStructures.Tests.CommonModels;
using TcfOss.DataStructures.ValueCollections;

namespace TcfOss.DataStructures.Tests.ValueCollections;

public class ValueDictTests
{
    [Fact]
    public void ConstructFromOtherValueDict()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>(dict);

        Assert.Equal(dict, dict2);
        Assert.NotSame(dict, dict2);
        Assert.Equal(dict.GetHashCode(), dict2.GetHashCode());

        foreach (KeyValuePair<string, int> kvp in dict)
        {
            Assert.True(dict2.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, dict2[kvp.Key]);
        }
    }

    [Fact]
    public void ConstructFromOtherValueDictNotEqual()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>(dict);

        Assert.Equal(dict, dict2);
        Assert.NotSame(dict, dict2);
        Assert.Equal(dict.GetHashCode(), dict2.GetHashCode());

        dict2["three"] = 4;
        Assert.NotEqual(dict, dict2);
        Assert.NotSame(dict, dict2);
        Assert.NotEqual(dict.GetHashCode(), dict2.GetHashCode());

        foreach (KeyValuePair<string, int> kvp in dict)
        {
            if (kvp.Key != "three")
            {
                Assert.True(dict2.ContainsKey(kvp.Key));
                Assert.Equal(kvp.Value, dict2[kvp.Key]);
            }
            else
            {
                Assert.True(dict2.ContainsKey(kvp.Key));
                Assert.NotEqual(kvp.Value, dict2[kvp.Key]);
            }
        }
    }

    [Fact]
    public void ConstructFromRegularDict()
    {
        var dict = new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>(dict);

        Assert.Equal(dict, dict2);
        Assert.NotSame(dict, dict2);

        foreach (KeyValuePair<string, int> kvp in dict)
        {
            Assert.True(dict2.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, dict2[kvp.Key]);
        }
    }

    [Fact]
    public void ConstructFromKeyValuePairs()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>(new[]
        {
            new KeyValuePair<string, int>("one", 1),
            new KeyValuePair<string, int>("two", 2),
            new KeyValuePair<string, int>("three", 3)
        });

        Assert.Equal(dict, dict2);
        Assert.NotSame(dict, dict2);

        foreach (KeyValuePair<string, int> kvp in dict)
        {
            Assert.True(dict2.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, dict2[kvp.Key]);
        }
    }

    [Fact]
    public void BuildElements()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>()
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        Assert.Equal(dict, dict2);
        Assert.Equal(dict, (object)dict2);
        Assert.Equal(dict.GetHashCode(), dict2.GetHashCode());

        foreach (KeyValuePair<string, int> kvp in dict)
        {
            Assert.True(dict2.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, dict2[kvp.Key]);
        }
    }

    [Fact]
    public void NotEqualByCount()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
        };

        Assert.NotEqual(dict, dict2);
    }

    [Fact]
    public void NotEqualByKeys()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        var dict2 = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["four"] = 3
        };

        Assert.NotEqual(dict, dict2);
    }

    [Fact]
    public void NotEqualNull()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        Assert.False(dict.Equals(null));
    }

    [Fact]
    public void CloneWorks()
    {
        var dict = new ValueDict<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3
        };

        ValueDict<string, int> cloned = dict.Clone();

        Assert.Equal(dict, cloned);
        Assert.NotSame(dict, cloned);

        object cloned2 = (dict as ICloneable)!.Clone();

        Assert.Equal(dict, cloned2);
        Assert.NotSame(dict, cloned2);
    }

    [Fact]
    public void CloneWithNonCloneableValuesWorks()
    {
        var dict = new ValueDict<string, TestRec>
        {
            ["rec1"] = new TestRec("val1"),
            ["rec2"] = new TestRec("val2"),
            ["rec3"] = new TestRec("val3"),
        };

        ValueDict<string, TestRec> cloned = dict.Clone();
        Assert.Equal(dict, cloned);
        Assert.NotSame(dict, cloned);

        foreach (KeyValuePair<string, TestRec> kvp in dict)
        {
            Assert.True(cloned.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, cloned[kvp.Key]);
            Assert.Same(kvp.Value, cloned[kvp.Key]);
        }

        object cloned2 = (dict as ICloneable)!.Clone();
        Assert.Equal(dict, cloned2);
        Assert.NotSame(dict, cloned2);
    }

    [Fact]
    public void CloneWithCloneableValuesWorks()
    {
        var dict = new ValueDict<string, CloneableTestRec>
        {
            ["rec1"] = new CloneableTestRec("val1"),
            ["rec2"] = new CloneableTestRec("val2"),
            ["rec3"] = new CloneableTestRec("val3"),
        };

        ValueDict<string, CloneableTestRec> cloned = dict.Clone();
        Assert.Equal(dict, cloned);
        Assert.NotSame(dict, cloned);

        foreach (KeyValuePair<string, CloneableTestRec> kvp in dict)
        {
            Assert.True(cloned.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, cloned[kvp.Key]);
            Assert.NotSame(kvp.Value, cloned[kvp.Key]);
        }

        object cloned2 = (dict as ICloneable)!.Clone();
        Assert.Equal(dict, cloned2);
        Assert.NotSame(dict, cloned2);
    }

    [Fact]
    public void CloneWithNonCloneableKeysWorks()
    {
        var dict = new ValueDict<TestRec, int>
        {
            [new TestRec("key1")] = 1,
            [new TestRec("key2")] = 2,
            [new TestRec("key3")] = 3,
        };

        ValueDict<TestRec, int> cloned = dict.Clone();
        Assert.Equal(dict, cloned);
        Assert.NotSame(dict, cloned);

        foreach (KeyValuePair<TestRec, int> kvp in dict)
        {
            Assert.True(cloned.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, cloned[kvp.Key]);
            Assert.Same(kvp.Key, cloned.Keys.First(k => k.Equals(kvp.Key)));
        }

        object cloned2 = (dict as ICloneable)!.Clone();
        Assert.Equal(dict, cloned2);
        Assert.NotSame(dict, cloned2);
    }

    [Fact]
    public void CloneWithCloneableKeysWorks()
    {
        var dict = new ValueDict<CloneableTestRec, int>
        {
            [new CloneableTestRec("key1")] = 1,
            [new CloneableTestRec("key2")] = 2,
            [new CloneableTestRec("key3")] = 3,
        };

        ValueDict<CloneableTestRec, int> cloned = dict.Clone();
        Assert.Equal(dict, cloned);
        Assert.NotSame(dict, cloned);

        foreach (KeyValuePair<CloneableTestRec, int> kvp in dict)
        {
            Assert.True(cloned.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, cloned[kvp.Key]);
            Assert.NotSame(kvp.Key, cloned.Keys.First(k => k.Equals(kvp.Key)));
        }
    }
}
