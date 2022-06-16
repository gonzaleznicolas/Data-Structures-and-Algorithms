using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(String[] args)
    {
        var minHeap = new MinHeap<KeyValuePair>();
        minHeap.Insert(new KeyValuePair(45, string.Empty));
        minHeap.Insert(new KeyValuePair(50, string.Empty));
        minHeap.Insert(new KeyValuePair(14, string.Empty));
        minHeap.Insert(new KeyValuePair(143, string.Empty));
        minHeap.Insert(new KeyValuePair(141, string.Empty));
        minHeap.Insert(new KeyValuePair(112, string.Empty));
        minHeap.Insert(new KeyValuePair(134, string.Empty));
        minHeap.Insert(new KeyValuePair(126, string.Empty));
        minHeap.Insert(new KeyValuePair(15, string.Empty));
        minHeap.Insert(new KeyValuePair(17, string.Empty));
        minHeap.Insert(new KeyValuePair(184, string.Empty));
        minHeap.Insert(new KeyValuePair(18, string.Empty));
        minHeap.Insert(new KeyValuePair(11, string.Empty));
        minHeap.Insert(new KeyValuePair(112, string.Empty));
        minHeap.Insert(new KeyValuePair(45, string.Empty));
        minHeap.Insert(new KeyValuePair(50, string.Empty));
        minHeap.Insert(new KeyValuePair(4, string.Empty));
        minHeap.Insert(new KeyValuePair(43, string.Empty));
        minHeap.Insert(new KeyValuePair(41, string.Empty));
        minHeap.Insert(new KeyValuePair(12, string.Empty));
        minHeap.Insert(new KeyValuePair(34, string.Empty));
        minHeap.Insert(new KeyValuePair(26, string.Empty));
        minHeap.Insert(new KeyValuePair(5, string.Empty));
        minHeap.Insert(new KeyValuePair(7, string.Empty));
        minHeap.Insert(new KeyValuePair(84, string.Empty));
        minHeap.Insert(new KeyValuePair(8, string.Empty));
        minHeap.Insert(new KeyValuePair(1, string.Empty));
        minHeap.Insert(new KeyValuePair(12, string.Empty));

        while(minHeap.Count > 0) Console.WriteLine(minHeap.DeleteMin().Key);

        Console.WriteLine();

        var maxHeap = new MaxHeap<KeyValuePair>();
        maxHeap.Insert(new KeyValuePair(45, string.Empty));
        maxHeap.Insert(new KeyValuePair(50, string.Empty));
        maxHeap.Insert(new KeyValuePair(4, string.Empty));
        maxHeap.Insert(new KeyValuePair(43, string.Empty));
        maxHeap.Insert(new KeyValuePair(41, string.Empty));
        maxHeap.Insert(new KeyValuePair(12, string.Empty));
        maxHeap.Insert(new KeyValuePair(34, string.Empty));
        maxHeap.Insert(new KeyValuePair(26, string.Empty));
        maxHeap.Insert(new KeyValuePair(5, string.Empty));
        maxHeap.Insert(new KeyValuePair(7, string.Empty));
        maxHeap.Insert(new KeyValuePair(84, string.Empty));
        maxHeap.Insert(new KeyValuePair(8, string.Empty));
        maxHeap.Insert(new KeyValuePair(1, string.Empty));
        maxHeap.Insert(new KeyValuePair(12, string.Empty));

        while(maxHeap.Count > 0) Console.WriteLine(maxHeap.DeleteMax().Key);
    }
}

public class MaxHeap<T> where T : IEquatable<T>, IComparable<T> {

    public int Count { get { return List.Count; } }

    private List<T> List { get; set; }

    public MaxHeap() {
        List = new List<T>();
    }

    public MaxHeap(int initialCapacity) {
        List = new List<T>(initialCapacity);
    }

    public void Insert(T value) {
        if (value == null) {
            throw new ArgumentException("Cannot insert null");
        }

        // insert at the end
        List.Add(value);

        // swap up until heap property is restored
        var i = List.Count - 1;
        T temp;
        while (i != 0 && List[Parent(i)].CompareTo(List[i]) < 0) {
            temp = List[Parent(i)];
            List[Parent(i)] = List[i];
            List[i] = temp;
            i = Parent(i);
        }
    }

    public T DeleteMax() {
        if (List.Count == 0) {
            throw new InvalidOperationException("MinHeap is empty.");
        }

        var max = List[0];

        // copy last element to root
        List[0] = List[List.Count - 1];

        // remove last element
        List.RemoveAt(List.Count - 1);

        // swap down with bigger child until heap property is restored
        int i = 0;
        while (HasBiggerChildren(i)) {
            if (HasLeft(i) && !HasRight(i)) {
                i = SwapWithLeft(i);
            } else if (HasRight(i) && !HasLeft(i)) {
                i = SwapWithRight(i);
            } else { // has left and right
                if (List[Left(i)].CompareTo(List[Right(i)]) > 0) {
                    i = SwapWithLeft(i);
                } else {
                    i = SwapWithRight(i);
                }
            }
        }

        return max;
    }

    private int SwapWithLeft(int i) {
        var temp = List[i];
        List[i] = List[Left(i)];
        List[Left(i)] = temp;
        return Left(i);
    }

    private int SwapWithRight(int i) {
        var temp = List[i];
        List[i] = List[Right(i)];
        List[Right(i)] = temp;
        return Right(i);
    }

    private bool HasBiggerChildren(int i) {
        return (HasLeft(i) && List[Left(i)].CompareTo(List[i]) > 0) || (HasRight(i) && List[Right(i)].CompareTo(List[i]) > 0);
    }

    private bool HasLeft(int i) {
        return Left(i) <= List.Count - 1;
    }

    private bool HasRight(int i) {
        return Right(i) <= List.Count - 1;
    }

    private int Parent(int i) {
        return (i-1) / 2;
    }

    private int Left(int i) {
        return 2*i + 1;
    }

    private int Right(int i) {
        return 2*i + 2;
    }
}

public class MinHeap<T> where T : IEquatable<T>, IComparable<T> {

    public int Count { get { return List.Count; } }

    private List<T> List { get; set; }

    public MinHeap() {
        List = new List<T>();
    }

    public MinHeap(int initialCapacity) {
        List = new List<T>(initialCapacity);
    }

    public void Insert(T value) {
        if (value == null) {
            throw new ArgumentException("Cannot insert null");
        }

        // insert at the end
        List.Add(value);

        // swap up until heap property is restored
        var i = List.Count - 1;
        T temp;
        while (i != 0 && List[Parent(i)].CompareTo(List[i]) > 0) {
            temp = List[Parent(i)];
            List[Parent(i)] = List[i];
            List[i] = temp;
            i = Parent(i);
        }
    }

    public T DeleteMin() {
        if (List.Count == 0) {
            throw new InvalidOperationException("MinHeap is empty.");
        }

        var min = List[0];

        // copy last element to root
        List[0] = List[List.Count - 1];

        // remove last element
        List.RemoveAt(List.Count - 1);

        // swap down with smaller child until heap property is restored
        int i = 0;
        while (HasSmallerChildren(i)) {
            if (HasLeft(i) && !HasRight(i)) {
                i = SwapWithLeft(i);
            } else if (HasRight(i) && !HasLeft(i)) {
                i = SwapWithRight(i);
            } else { // has left and right
                if (List[Left(i)].CompareTo(List[Right(i)]) < 0) {
                    i = SwapWithLeft(i);
                } else {
                    i = SwapWithRight(i);
                }
            }
        }

        return min;
    }

    private int SwapWithLeft(int i) {
        var temp = List[i];
        List[i] = List[Left(i)];
        List[Left(i)] = temp;
        return Left(i);
    }

    private int SwapWithRight(int i) {
        var temp = List[i];
        List[i] = List[Right(i)];
        List[Right(i)] = temp;
        return Right(i);
    }

    private bool HasSmallerChildren(int i) {
        return (HasLeft(i) && List[Left(i)].CompareTo(List[i]) < 0) || (HasRight(i) && List[Right(i)].CompareTo(List[i]) < 0);
    }

    private bool HasLeft(int i) {
        return Left(i) <= List.Count - 1;
    }

    private bool HasRight(int i) {
        return Right(i) <= List.Count - 1;
    }

    private int Parent(int i) {
        return (i-1) / 2;
    }

    private int Left(int i) {
        return 2*i + 1;
    }

    private int Right(int i) {
        return 2*i + 2;
    }
}

public class KeyValuePair : IEquatable<KeyValuePair>, IComparable<KeyValuePair> {
    public int Key { get; set; }
    public string Value { get; set; }

    public KeyValuePair(int k, string v) {
        Key = k;
        Value = v;
    }

	public override int GetHashCode()
	{
		return Key.GetHashCode();
	}

    public bool Equals(KeyValuePair other) {
        return Key == other.Key;
    }

    public int CompareTo(KeyValuePair other) {
        if (Key < other.Key) {
            return -1;
        } else if (Key > other.Key) {
            return 1;
        } else {
            return 0;
        }
    }
}

