using System;
using System.Collections;
using System.Collections.Generic;

namespace OrderHub.Collections;

public class CircularBuffer<T> : IEnumerable<T>
{
    private readonly T[] buffer;
    private int next = 0;
    private int size = 0;
    private readonly int capacity;

    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        this.capacity = capacity;
        buffer = new T[capacity];
    }

    public int Count => size;
    public bool IsFull => size == capacity;

    public void Add(T item)
    {
        buffer[next] = item;
        next = (next + 1) % capacity;

        if (size < capacity)
        {
            size++;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (size == 0)
        {
            yield break;
        }

        int index = (next - size + capacity) % capacity;

        for (int i = 0; i < size; i++)
        {
            yield return buffer[index];
            index = (index + 1) % capacity;
        }
    }

    public IEnumerable<T> Reverse()
    {
        if (size == 0)
        {
            yield break;
        }

        int index = (next - 1 + capacity) % capacity;

        for (int i = 0; i < size; i++)
        {
            yield return buffer[index];
            index = (index - 1 + capacity) % capacity;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
