# DataStructures

A collection of special-use data structures.

## Value Collections

Containers for collections of values, with value semantics (i.e. two instances are considered equal
if they contain the same values). These include:

- `ValueList<T>`
- `ValueArray<T>`
- `ValueDict<TKey, TValue>`

Two instances of `ValueList<T>` or `ValueArray<T>` are equal if they contain the same values in
the same order. Two instances of `ValueDict<TKey, TValue>` are equal if they contain the same
key-value pairs, regardless of order.

The collections all implement `IEquatable<T>`, and all keys and values must also implement
`IEquatable<T>`.

`ValueList<T>` and `ValueDict<TKey, TValue>` implement `ICloneable`. Cloning one of these
collections creates a new instance with the same values (and keys for `ValueDict<TKey, TValue>`).
When constructing the clone, all values and keys in the original collection that implement
`ICloneable` are also cloned.

The `ValueArray<T>` type is also immutable.


## Reporting Issues and Contributing

Issues for this project are tracked on [IssueTracker](https://issues.tcflanagan.net/data-structures). If you encounter any bugs or have feature requests, please submit them there.

Contributions are welcome. Follow the usual fork-and-pull request workflow. Before submitting a pull request, make sure

1. Code is linted: run `dotnet format --severity info --verify-no-changes` in the repo root.
2. All existing tests succeed.
3. Any new code includes appropriate tests.
4. Documentation is updated as necessary (and—especially if AI generates the updates—spaces are removed around any em-dashes).

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

