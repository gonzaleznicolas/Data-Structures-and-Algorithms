using System;

public class Program
{
	public static void Main(String[] args)
	{
		var d = new Dictionary<string, string>();
		Console.WriteLine(d.Insert("1", "1"));
		Console.WriteLine(d.Insert("dsa", "dsa"));
		Console.WriteLine(d.Insert("Nico", "Nico"));
		Console.WriteLine(d.Insert("Nata", "Nata"));
		Console.WriteLine(d.Insert("Cata", "Cata"));
		Console.WriteLine(d.Insert("Mama", "Mama"));
		Console.WriteLine(d.Insert("Papa", "Papa"));
		Console.WriteLine(d.Insert("Nico1", "Nico1"));
		Console.WriteLine(d.Insert("Nata1", "Nata1"));
		Console.WriteLine(d.Insert("Cata1", "Cata1"));
		Console.WriteLine(d.Insert("Mama1", "Mama1"));
		Console.WriteLine(d.Insert("Papa1", "Papa1"));
		Console.WriteLine(d.Insert("Nico2", "Nico2"));
		Console.WriteLine(d.Insert("Nata2", "Nata2"));
		Console.WriteLine(d.Insert("Cata2", "Cata2"));
		Console.WriteLine(d.Insert("Mama2", "Mama2"));
		Console.WriteLine(d.Insert("Papa2", "Papa2"));
		Console.WriteLine(d.Insert("Nico3", "Nico3"));
		Console.WriteLine(d.Insert("Nata3", "Nata3"));
		Console.WriteLine(d.Insert("Cata3", "Cata3"));
		Console.WriteLine(d.Insert("Mama3", "Mama3"));
		Console.WriteLine(d.Insert("Papa3", "Papa3"));
		Console.WriteLine(d.Insert("Nico3", "Nico3"));
		Console.WriteLine(d.Insert("Nata3", "Nata3"));
		Console.WriteLine(d.Insert("Cata3", "Cata3"));
		Console.WriteLine(d.Insert("Mama3", "Mama3"));
		Console.WriteLine(d.Insert("Papa3", "Papa3"));

		Console.WriteLine(d.GetByKey("Mama3", out var mama3));
		Console.WriteLine(mama3);
		Console.WriteLine(d.GetByKey("dx", out var dx));
		Console.WriteLine(dx);

		Console.WriteLine(d.Remove("Nico"));
		Console.WriteLine(d.Remove("Nico7"));
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

	public bool Insert(TKey key, TValue value)
	{
		var ll = LinkedLists[KeyToIndex(key)];
		return ll.Insert(key, value);
	}

	public bool GetByKey(TKey key, out TValue value)
	{
		var ll = LinkedLists[KeyToIndex(key)];
		return ll.GetByKey(key, out value);
	}

	public bool Remove(TKey key)
	{
		var ll = LinkedLists[KeyToIndex(key)];
		return ll.Remove(key);
	}

	private int KeyToIndex(TKey key)
	{
		var hash = key.GetHashCode();
		var positive = hash & 0x7FFFFFFF;
		return positive % LinkedLists.Length;
	}

	public class LinkedList
	{
		private Node Head { get; set; }

		public bool IsEmpty {
			get
			{
				return Head == null;
			}
		}

		public bool Insert(TKey key, TValue value)
		{
			var newNode = new Node(key, value);
			if (Head == null)
			{
				Head = newNode;
			}
			else
			{
				// The list is not empty, iterate through to make sure the key does not already exist
				Node pointer = Head;
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

		public bool GetByKey(TKey key, out TValue value)
		{
			Node pointer = Head;
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
				Node pointer = Head;
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
			public TValue Value { get; set; }
			public Node Next { get; set; }

			public Node(TKey key, TValue value)
			{
				Key = key;
				Value = value;
			}
		}
	}
}
