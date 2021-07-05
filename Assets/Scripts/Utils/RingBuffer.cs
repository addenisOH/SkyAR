using System.Collections.Generic;
using System;
using System.Linq;

public class RingBuffer<T>
{
    private readonly Queue<T> buffer;
    private readonly int size;

    public RingBuffer(int bufferSize)
    {
        if (bufferSize <= 0)
        {
            throw new ArgumentException("Buffer size cannot be negative or zero");
        }
        size = bufferSize;
        buffer = new Queue<T>();
    }

    public void Add(T t)
    {
        if (buffer.Count == size)
        {
            buffer.Dequeue();
        }

        buffer.Enqueue(t);
    }

    public bool Contains(T t)
    {
        return buffer.Contains(t);
    }

    public bool Contains(Func<T, bool> condition)
    {
        T item = buffer.Where(condition).FirstOrDefault();

        if (item != null)
            return true;
        else
            return false;
    }

    public int Size()
    {
        return buffer.Count;
    }

    public float FloatMean()
    {
        double mean = 0.0;
        foreach (var item in buffer)
        {
            mean += Convert.ToDouble(item);
        }

        mean /= (double)Size();
        return (float)mean;
    }
    public void Clear()
    {
        buffer.Clear();
    }
}
