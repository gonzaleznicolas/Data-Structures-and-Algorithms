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
	}
}


public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    private IEntry[] Entries;
    private int NumNonNil;

    public Dictionary(int capacity = 10)
    {
        Entries = new IEntry[capacity];
        NumNonNil = 0;
        for (int i = 0; i < Entries.Length; i++) Entries[i] = new Nil();
    }

    public bool Insert(TKey key, TValue value)
	{
		if (Insert(Entries, key, value))
		{
			NumNonNil++;
            RehashIfNecessary();
			return true;
		}
		return false;
	}

    private bool Insert(IEntry[] entries, TKey key, TValue value)
    {
        if (key == null) {
            return false;
        }
        var attemptNumber = 0;
        while (attemptNumber < entries.Length)
        {
            var index = GetProbeIndex(key, attemptNumber, entries.Length);
            if (entries[index].GetType() == typeof(Nil))
            {
                entries[index] = new Entry(key, value);
                return true;
            }
            else if (entries[index].GetType() == typeof(Del))
            {
                attemptNumber++;
            }
            else
            {
                Entry entry = (Entry)entries[index];
                if (entry.Key.Equals(key))
                {
                    // key already exists
                    return false;
                }
                else
                {
                    attemptNumber++;
                }
            }
        }
        // if here, all the positions are either DEL or occupied
        return false;
    }

    public bool GetByKey(TKey key, out TValue value)
	{
		if (key == null) {
            value = default(TValue);
            return false;
        }
        var attemptNumber = 0;
        while (attemptNumber < Entries.Length)
        {
            var index = GetProbeIndex(key, attemptNumber, Entries.Length);
            if (Entries[index].GetType() == typeof(Entry))
            {
                Entry entry = (Entry)Entries[index];
                if (entry.Key.Equals(key))
                {
                    value = entry.Value;
                    return true;
                }
                else
                {
                    attemptNumber++;
                }
            }
            else if (Entries[index].GetType() == typeof(Del))
            {
                attemptNumber++;
            }
            else
            {
                // NIL
                value = default(TValue);
                return false;
            }
        }
        value = default(TValue);
        return false;
	}

    private void RehashIfNecessary()
    {
        if (NumNonNil > Entries.Length / 2)
        {
            var newEntries = new IEntry[Entries.Length * 2];
            for (int i = 0; i < newEntries.Length; i++) newEntries[i] = new Nil();
            foreach(var spot in Entries)
            {
                if (spot.GetType() == typeof(Entry))
                {
                    var entry = (Entry)spot;
                    Insert(newEntries, entry.Key, entry.Value);
                }
            }
            NumNonNil = 0;
            Entries = newEntries;
        }
    }

    private int GetProbeIndex(TKey key, int attemptNumber, int numberOfPositions)
    {
        var hash = key.GetHashCode();
        var positive = hash & 0x7FFFFFFF;
        return (positive + attemptNumber) % numberOfPositions;
    }

    private interface IEntry {}

    private class Nil : IEntry {}

    private class Del : IEntry {}

    private class Entry : IEntry
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
