using System;
using System.Collections.Generic;

public class Program
{
	public static void Main(String[] args)
	{
		var graph = new UndirectedGraph<string>();
		Console.WriteLine(graph.AddVertex("a"));
		Console.WriteLine(graph.AddVertex("b"));
		Console.WriteLine(graph.AddVertex("c"));
		Console.WriteLine(graph.AddVertex("d"));
		Console.WriteLine(graph.AddVertex("e"));
		Console.WriteLine(graph.AddVertex("f"));
		Console.WriteLine(graph.AddVertex("g"));
		Console.WriteLine(graph.AddEdge("a", "b", 2));
		Console.WriteLine(graph.AddEdge("a", "c", 1));
		Console.WriteLine(graph.AddEdge("b", "c", 1));
		Console.WriteLine(graph.AddEdge("b", "e", 5));
		Console.WriteLine(graph.AddEdge("b", "d", 3));
		Console.WriteLine(graph.AddEdge("c", "d", 2));
		Console.WriteLine(graph.AddEdge("c", "f", 4));
		Console.WriteLine(graph.AddEdge("d", "e", 2));
		Console.WriteLine(graph.AddEdge("d", "f", 1));
		Console.WriteLine(graph.AddEdge("e", "f", 3));
		Console.WriteLine(graph.AddEdge("e", "g", 1));
		Console.WriteLine(graph.AddEdge("f", "g", 2));
	}
}

public class UndirectedGraph<T> where T : IEquatable<T> {
	private Dictionary<T, Dictionary<T, int>> Vertices { get; set; }

	public UndirectedGraph() {
		Vertices = new Dictionary<T, Dictionary<T, int>>();
	}

	public bool AddVertex(T value) {
		return Vertices.TryAdd(value, new Dictionary<T, int>());
	}

	// Returns true if start and end are vertices in the graph and the edge a-b did not already exist and was successfully added
	// False otherwise.
	public bool AddEdge(T a, T b, int weight = 1) {
		if (!Vertices.TryGetValue(a, out var aAdjacencyDict)
			|| !Vertices.TryGetValue(b, out var bAdjacencyDict)
			|| aAdjacencyDict.ContainsKey(b)
			|| bAdjacencyDict.ContainsKey(a)) {
			return false;
		}

		aAdjacencyDict.Add(b, weight);
		bAdjacencyDict.Add(a, weight);
		return true;
	}
}
