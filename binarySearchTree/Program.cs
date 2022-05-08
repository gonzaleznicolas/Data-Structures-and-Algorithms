using System;

class Program {
	static void Main(string[] args) {
		Console.WriteLine(new KeyType(1).Equals(new KeyType(2)));
	}
}

public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey> {
	private Node Root { get; set; }
	private int Count { get; set; }

	public BinarySearchTree() {
		Root = null;
		Count = 0;
	}

	// Searches for key in tree rooted at subtreeRoot. subtreeRoot cannot be null.
	// If found returns node, else return last node visited in search
	private Node SearchAsFarAsPossible(Node subtreeRoot, TKey key) {
		if (subtreeRoot == null) {
			throw new InvalidOperationException("the root cannot be null");
		}

		if (subtreeRoot.Key.Equals(key)) {
			return subtreeRoot;
		}

		Node toReturn = null;
		var pointer = subtreeRoot;
		while (toReturn == null) {
			if (key.CompareTo(pointer.Key) < 0) {
				if (pointer.Left == null) {
					toReturn = pointer;
				} else if (pointer.Left.Key.Equals(key)) {
					toReturn = pointer.Left;
				} else {
					pointer = pointer.Left;
				}
			} else if (key.CompareTo(pointer.Key) > 0) {
				if (pointer.Right == null) {
					toReturn = pointer;
				} else if (pointer.Right.Key.Equals(key)) {
					toReturn = pointer.Right;
				} else {
					pointer = pointer.Right;
				}
			}
		}
		return toReturn;
	}

	private class Node {
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

	public override int GetHashCode() {
		return Num.GetHashCode();
	}

	public bool Equals(KeyType other) {
		return other.Num == Num;
	}

	public int CompareTo(KeyType other) {
		if (Num < other.Num) return -1;
		else if (Num > other.Num) return 1;
		else return 0;
	}
}
