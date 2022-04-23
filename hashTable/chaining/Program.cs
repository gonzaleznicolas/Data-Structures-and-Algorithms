public class Program
{
	public static void Main(String[] args)
	{
		var ll = new Dictionary<string, string>();
		Console.WriteLine(ll.Insert("1", "1"));
	}
}


public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
	private LinkedList[] LinkedLists { get; set; }

	public Dictionary(int initialCapacity = 10)
	{
		LinkedLists = new LinkedList[initialCapacity];
		for (int i = 0; i < LinkedLists.Length; i++) LinkedLists[i] = new LinkedList();
	}

	public bool Insert(TKey key, TValue? value) {
		var ll = LinkedLists[KeyToIndex(key)];
		return ll.Insert(key, value);
	}

	private int KeyToIndex(TKey key) {
		var hash = key.GetHashCode();
		var positive = hash & 0x7FFFFFFF;
		return positive % LinkedLists.Length;
	}

	public class LinkedList
	{
		private Node? Head { get; set; }

		public bool IsEmpty {
			get
			{
				return Head == null;
			}
		}

		public bool Insert(TKey key, TValue? value)
		{
			var newNode = new Node(key, value);
			if (Head == null)
			{
				Head = newNode;
			}
			else
			{
				// The list is not empty, iterate through to make sure the key does not already exist
				Node? pointer = Head;
				while (pointer != null)
				{
					if (pointer.Key.Equals(newNode.Key))
					{
						return false;
					}
					else
					{
						if (pointer.Next == null) {
							pointer.Next = newNode;
							pointer = null;
						}
						else
						{
							pointer = pointer.Next;
						}
					}
				}
			}
			return true;
		}

		public bool GetByKey(TKey key, out TValue? value)
		{
			Node? pointer = Head;
			while (pointer != null) {
				if (pointer.Key.Equals(key)) {
					value = pointer.Value;
					return true;
				} else {
					pointer = pointer.Next;
				}
			}
			value = default(TValue);
			return false;
		}

		public bool Remove(TKey key)
		{
			if (Head == null) {
				return false;
			} else if (Head.Key.Equals(key)) {
				Head = Head.Next;
				return true;
			} else {
				// The list is not empty and key is not at the head
				Node? pointer = Head;
				while (pointer.Next != null)
				{
					if (pointer.Next.Key.Equals(key))
					{
						pointer.Next = pointer.Next.Next;
						return true;
					} else
					{
						pointer = pointer.Next;
					}
				}
				return false;
			}
		}

		private class Node
		{
			public TKey Key { get; set; }
			public TValue? Value { get; set; }
			public Node? Next { get; set; }

			public Node(TKey key, TValue? value)
			{
				Key = key;
				Value = value;
			}
		}
	}
}

/*
int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
int targetBucket = hashCode % buckets.Length;
*/