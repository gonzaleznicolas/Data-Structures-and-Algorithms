using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{
    public static void Main(String[] args)
    {
        // Declarations
        var priorityQueue = new PriorityQueue<string, TypeA>();
        priorityQueue = new PriorityQueue<string, TypeA>(10);

        priorityQueue.Enqueue("two", new TypeA(2));
        priorityQueue.Enqueue("one", new TypeA(1));
        priorityQueue.Enqueue("three", new TypeA(3));
        Console.WriteLine(priorityQueue.Peek()); // one
        Console.WriteLine(priorityQueue.Dequeue()); // one
        Console.WriteLine(priorityQueue.Dequeue()); // two
    }
}

struct TypeA : IEquatable<TypeA>, IComparable<TypeA>
{
    public double Value { get; set; }
    public TypeA(double v) { Value = v; }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    public bool Equals(TypeA other)
    {
        return other.Value == this.Value;
    }
    public int CompareTo(TypeA other)
    {
        if (Value < other.Value) return -1;
        if (Value > other.Value) return 1;
        else return 0; 
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}