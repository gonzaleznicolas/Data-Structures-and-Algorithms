using System;
using System.Collections.Generic;

public class Program
{
	public static void Main(String[] args)
	{
		var graph = new DirectedGraph<string>();
		Console.WriteLine(graph.AddVertex("a"));
		Console.WriteLine(graph.AddVertex("b"));
		Console.WriteLine(graph.AddVertex("c"));
		Console.WriteLine(graph.AddVertex("d"));
		Console.WriteLine(graph.AddVertex("e"));
		Console.WriteLine(graph.AddVertex("f"));
		Console.WriteLine(graph.AddVertex("g"));
		Console.WriteLine(graph.AddVertex("h"));
		Console.WriteLine(graph.AddVertex("i"));
		Console.WriteLine(graph.AddEdge("a", "b", 5));
		Console.WriteLine(graph.AddEdge("a", "c", 3));
		Console.WriteLine(graph.AddEdge("a", "e", 1));
		Console.WriteLine(graph.AddEdge("b", "d", 2));
		Console.WriteLine(graph.AddEdge("c", "b", 1));
		Console.WriteLine(graph.AddEdge("c", "d", 1));
		Console.WriteLine(graph.AddEdge("d", "a", 1));
		Console.WriteLine(graph.AddEdge("d", "g", 2));
		Console.WriteLine(graph.AddEdge("d", "h", 1));
		Console.WriteLine(graph.AddEdge("e", "a", 1));
		Console.WriteLine(graph.AddEdge("e", "h", 4));
		Console.WriteLine(graph.AddEdge("e", "i", 7));
		Console.WriteLine(graph.AddEdge("f", "b", 3));
		Console.WriteLine(graph.AddEdge("f", "g", 1));
		Console.WriteLine(graph.AddEdge("g", "c", 3));
		Console.WriteLine(graph.AddEdge("g", "i", 2));
		Console.WriteLine(graph.AddEdge("h", "c", 2));
		Console.WriteLine(graph.AddEdge("h", "f", 2));
		Console.WriteLine(graph.AddEdge("h", "g", 2));

		Console.WriteLine();
	}
}

public class DirectedGraph<T> where T : IEquatable<T> {
	private Dictionary<T, Dictionary<T, int>> Vertices { get; set; }

	public DirectedGraph() {
		Vertices = new Dictionary<T, Dictionary<T, int>>();
	}

	public bool AddVertex(T value) {
		return Vertices.TryAdd(value, new Dictionary<T, int>());
	}

	// Returns true if start and end are vertices in the graph and the edge a->b did not already exist and was successfully added
	// False otherwise.
	public bool AddEdge(T a, T b, int weight = 1) {
		Dictionary<T, int> aAdjacencyDict;
		Dictionary<T, int> bAdjacencyDict;
		if (!Vertices.TryGetValue(a, out aAdjacencyDict)
			|| !Vertices.TryGetValue(b, out bAdjacencyDict)
			|| aAdjacencyDict.ContainsKey(b)) {
			return false;
		}

		aAdjacencyDict.Add(b, weight);
		return true;
	}
}
