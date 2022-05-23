using System;

public class Program
{
    public static void Main(String[] args)
    {
        var trie = new Trie();
        trie.Insert("Nico");
        trie.Insert("Nata");
        trie.Insert("Cata");
        trie.Insert("Mama");
        trie.Insert("Papa");

        Console.WriteLine(trie.IsFullWord("Nico"));
        Console.WriteLine(trie.IsFullWord("Nic"));
        Console.WriteLine(trie.IsPrefix("Nico"));
        Console.WriteLine(trie.IsPrefix("Nic"));
    }
}

public class Trie {

    private Node Root;

    public Trie() {
        Root = new Node(null);
    }

    public bool IsPrefix(string s) {
        if (s == null) throw new ArgumentException("Cannot search for null string.");

        s = s.ToLower();
        var currentNode = Root;
        foreach (var c in s) {
            var childC = currentNode.GetChild(c);
            if (childC == null) {
                return false;
            }
            currentNode = childC;
        }

        return true;
    }

    public bool IsFullWord(string s) {
        if (s == null) throw new ArgumentException("Cannot search for null string.");

        s = s.ToLower();
        var currentNode = Root;
        foreach (var c in s) {
            var childC = currentNode.GetChild(c);
            if (childC == null) {
                return false;
            }
            currentNode = childC;
        }

        return currentNode.EndOfWord;
    }

    public void Insert(string s) {
        if (s == null) throw new ArgumentException("Cannot insert null string.");

        s = s.ToLower();
        var currentNode = Root;
        foreach (var c in s) {
            var childC = currentNode.GetChild(c);
            if (childC == null) {
                childC = currentNode.CreateChild(c);
            }
            currentNode = childC;
        }

        currentNode.EndOfWord = true;
    }

    private class Node {
        public char? Char { get; set; } // null if root

        public bool EndOfWord { get; set; }

        public Node[] Children { get; set; }

        public Node(char? c) {
            Char = c;
            Children = new Node[26];
        }

        public Node GetChild(char c) {
            return Children[CharToIndex(c)];
        }

        public Node CreateChild(char c) {
            var index = CharToIndex(c);
            Children[index] = new Node(c);
            return Children[index];
        }

        private int CharToIndex(char c) {
            return (int)c - 97;
        }
    }
}
