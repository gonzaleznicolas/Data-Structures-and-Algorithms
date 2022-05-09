using System;

class Program {
	static void Main(string[] args) {
		var bst = new BinarySearchTree<KeyType, string>();
		bst.Insert(new KeyType(50), "50");
		bst.Insert(new KeyType(25), "25");
		bst.Insert(new KeyType(75), "75");
		bst.Insert(new KeyType(20), "20");

		try { bst.Insert(new KeyType(20), "20"); } catch { Console.WriteLine("As expected, throws exception on inserting 20."); };
		bst.Insert(new KeyType(30), "30");
		bst.Insert(new KeyType(60), "60");
		bst.Insert(new KeyType(80), "80");

		Console.WriteLine(bst.Search(new KeyType(60)));
		Console.WriteLine(bst.Search(new KeyType(50)));
		try { bst.Search(new KeyType(22)); } catch { Console.WriteLine("As expected, throws exception on searching 22."); };
	}
}

public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey> {
	private Node Root { get; set; }
	private int Count { get; set; }

	public BinarySearchTree() {
		Root = null;
		Count = 0;
	}

	public TValue Search(TKey key) {
		var node = SearchAsFarAsPossible(Root, key);
		if (node.Key.Equals(key)) return node.Value;
		throw new InvalidOperationException("Key not found.");
	}

	public void Insert(TKey key, TValue value) {
		var newNode = new Node { Key = key, Value = value };
		if (Root == null) {
			Root = newNode;
			Count = 1;
			return;
		}

		var node = SearchAsFarAsPossible(Root, key);

		if (node.Key.Equals(key)) {
			// Key already exists
			throw new InvalidOperationException("Key already exists");
		}

		if (key.CompareTo(node.Key) < 0) {
			node.Left = newNode;
			newNode.Parent = node;
		} else {
			// key > node.Key
			node.Right = newNode;
			newNode.Parent = node;
		}
		Count++;
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

		var pointer = subtreeRoot;
		while (true) {
			if (key.CompareTo(pointer.Key) < 0) {
				if (pointer.Left == null) {
					return pointer;
				} else if (pointer.Left.Key.Equals(key)) {
					return pointer.Left;
				} else {
					pointer = pointer.Left;
				}
			} else if (key.CompareTo(pointer.Key) > 0) {
				if (pointer.Right == null) {
					return pointer;
				} else if (pointer.Right.Key.Equals(key)) {
					return pointer.Right;
				} else {
					pointer = pointer.Right;
				}
			}
		}
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
