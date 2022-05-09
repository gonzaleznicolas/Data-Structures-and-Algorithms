using System;
using System.Collections.Generic;
using System.Collections;

class Program {
	static void Main(string[] args) {
		var bst = new BinarySearchTree<KeyType, string>();
		bst.Insert(new KeyType(500), "500");
		bst.Insert(new KeyType(300), "300");
		bst.Insert(new KeyType(700), "700");
		bst.Insert(new KeyType(200), "200");
		bst.Insert(new KeyType(400), "400");
		bst.Insert(new KeyType(600), "600");
		bst.Insert(new KeyType(800), "800");
		bst.Insert(new KeyType(150), "150");
		bst.Insert(new KeyType(250), "250");
		bst.Insert(new KeyType(350), "350");
		bst.Insert(new KeyType(450), "450");
		bst.Insert(new KeyType(550), "550");
		bst.Insert(new KeyType(650), "650");
		bst.Insert(new KeyType(750), "750");
		bst.Insert(new KeyType(850), "850");
		bst.PrintInOrder();
		Console.WriteLine();
		foreach(var val in bst) {
			Console.WriteLine(val);
		}
	}
}

public class BinarySearchTree<TKey, TValue>: IEnumerable<TValue> where TKey : IComparable<TKey>, IEquatable<TKey> {
	private Node Root { get; set; }
	private int Count { get; set; }

	public BinarySearchTree() {
		Root = null;
		Count = 0;
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	public IEnumerator<TValue> GetEnumerator()
    {
        return InOrder(Root);
    }

	private IEnumerator<TValue> InOrder(Node node) {
		if (node.Left != null) {
			var leftEnumerator = InOrder(node.Left);
			while(leftEnumerator.MoveNext()) {
				var val = leftEnumerator.Current;
				yield return val;
			}
		}
		yield return node.Value;
		if (node.Right != null) {
			var rightEnumerator = InOrder(node.Right);
			while(rightEnumerator.MoveNext()) {
				var val = rightEnumerator.Current;
				yield return val;
			}
		}
	}

	public void Delete(TKey key) {
		var node = SearchAsFarAsPossible(Root, key);

		if (!node.Key.Equals(key)) throw new InvalidOperationException("Key not found.");

		if (node.NumChildren == 0) {
			RemoveNodeWithZeroChildren(node);
		} else if (node.NumChildren == 1) {
			RemoveNodeWithOneChild(node);
		} else {
			// node has 2 children
			var minInRightSubtree = FindAndDeleteMinFromSubtree(node.Right);
			node.Value = minInRightSubtree.Value;
			node.Key = minInRightSubtree.Key;
		}

		Count--;
	}

	private void RemoveNodeWithZeroChildren(Node node) {
		node.Parent.RemoveChild(node.Key);
		node.Parent = null;
	}

	private void RemoveNodeWithOneChild(Node node) {
		var onlyChild = node.OnlyChild;
		node.Parent.ReplaceChildWithKey(node.Key, onlyChild);
	}

	// subtreeRoot must not be null and must have 2 non null children
	private Node FindAndDeleteMinFromSubtree(Node subtreeRoot) {
		var min = FindMinFromSubtree(subtreeRoot);
		// min will definitely have 0 or 1 children
		if (min.NumChildren == 0)
			RemoveNodeWithZeroChildren(min);
		else if (min.NumChildren == 1)
			RemoveNodeWithOneChild(min);
		return min;
	}

	// subtreeRoot must not be null
	private Node FindMinFromSubtree(Node subtreeRoot) {
		Node pointer = subtreeRoot;
		while (true) {
			if (pointer.Left != null)
				pointer = pointer.Left;
			else
				return pointer;
		}
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

		public int NumChildren {
			get {
				var count = 0;
				if (Left != null) count++;
				if (Right != null) count++;
				return count;
			}
		}

		public Node OnlyChild {
			get {
				if (NumChildren == 1) {
					if (Left != null) return Left;
					if (Right != null) return Right;
				}
				throw new InvalidOperationException("Cannot get OnlyChild of a node with 0 or 2 children.");
			}
		}

		public void RemoveChild(TKey key) {
			if (key.Equals(Left.Key)) Left = null;
			else if (key.Equals(Right.Key)) Right = null;
			else throw new InvalidOperationException("Provided key is not a child of this node.");
		}

		public void ReplaceChildWithKey(TKey keyOfChild, Node replacement) {
			if (keyOfChild.Equals(Left.Key)) {
				Left = replacement;
				replacement.Parent = this;
			}
			else if (keyOfChild.Equals(Right.Key)) {
				Right = replacement;
				replacement.Parent = this;
			}
			else throw new InvalidOperationException("Provided key is not a child of this node.");
		}
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
