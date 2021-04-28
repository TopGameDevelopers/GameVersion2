using System.Collections;
using System.Collections.Generic;

public class SinglyLinkedList<T> : IEnumerable<T>
{
    public readonly T Value;
    private readonly SinglyLinkedList<T> _previous;
    private readonly int _length;

    public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
    {
        Value = value;
        _previous = previous;
        _length = previous?._length + 1 ?? 1;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;
        var pathItem = _previous;
        while (pathItem != null)
        {
            yield return pathItem.Value;
            pathItem = pathItem._previous;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}