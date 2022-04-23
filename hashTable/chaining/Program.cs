public class Program
{
	public static void Main(String[] args)
	{
		var ll = new Dictionary<int, int>.LinkedList();
		ll.Insert(1, 1);
		ll.Insert(2, 2);
		ll.Insert(3, 3);
		ll.Insert(4, 4);
		ll.Insert(5, 5);
		ll.Insert(6, 6);
		ll.Remove(1);
		ll.Remove(3);
		ll.Remove(10);
		ll.Remove(6);
		Console.WriteLine(ll.GetByKey(2, out int f));
	}
}


public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
	public class LinkedList
	{
		private Node? Head { get; set; }

		public void Insert(TKey key, TValue? value)
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
						throw new InvalidOperationException("An entry with that key already exists.");
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
	}

	public class Node
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

/*
int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
int targetBucket = hashCode % buckets.Length;
*/