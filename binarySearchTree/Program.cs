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
		bst.Insert(new KeyType(10), "10");
		bst.Insert(new KeyType(33), "33");

		bst.Delete(new KeyType(50));
		bst.Delete(new KeyType(10));
		bst.Delete(new KeyType(30));

		Console.WriteLine(bst.Search(new KeyType(60)));
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
