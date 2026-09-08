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
