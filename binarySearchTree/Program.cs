using System;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine(new KeyType(1).Equals(new KeyType(2)));
	}
}

public class BinarySearchTree<TKey, TValue>
{
	private class Node
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }

		public Node Parent { get; set; }

		public Node Left { get; set; }

		public Node Right { get; set; }
	}
}

public struct KeyType : IEquatable<KeyType>, IComparable<KeyType>
{
	public int Num { get; set; }

	public KeyType(int n) { Num = n; }

	public override int GetHashCode()
	{
		return Num.GetHashCode();
	}

	public bool Equals(KeyType other)
	{
		return other.Num == Num;
	}

	public int CompareTo(KeyType other)
	{
		if (Num < other.Num) return -1;
		else if (Num > other.Num) return 1;
		else return 0;
	}
}
