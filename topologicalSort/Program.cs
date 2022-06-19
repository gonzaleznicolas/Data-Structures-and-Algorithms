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

		Console.WriteLine(graph.AddEdge("a", "b", 5));
		Console.WriteLine(graph.AddEdge("a", "c", 3));
		Console.WriteLine(graph.AddEdge("b", "g", 2));
		Console.WriteLine(graph.AddEdge("c", "f", 1));
		Console.WriteLine(graph.AddEdge("d", "c", 1));
		Console.WriteLine(graph.AddEdge("d", "e", 2));
		Console.WriteLine(graph.AddEdge("e", "f", 1));
		Console.WriteLine(graph.AddEdge("f", "g", 3));
		//Console.WriteLine(graph.AddEdge("g", "b", 1));

		Console.WriteLine();
		List<string> topologicalSort;
		if (TopologicalSort(graph, out topologicalSort)) {
			topologicalSort.ForEach(e => Console.Write($"{e}, "));
			Console.WriteLine();
		} else {
			Console.WriteLine("No topological sort");
		}
	}

	// returns null if there is no topological sort
	public static bool TopologicalSort(DirectedGraph<string> graph, out List<string> topologicalSort) {
		var incoming = new Dictionary<string, int>(graph.Vertices.Count);
		foreach(var kvp in graph.Vertices) {
			incoming.Add(kvp.Key, 0);
		}

		foreach(var kvp1 in graph.Vertices) {
			foreach(var kvp2 in kvp1.Value) {
				incoming[kvp2.Key]++;
			}
		}

		var processNext = new Queue<string>(graph.Vertices.Count);
		topologicalSort = new List<string>(graph.Vertices.Count);

		foreach(var kvp in incoming) {
			if (kvp.Value == 0) {
				processNext.Enqueue(kvp.Key);
			}
		}

		while(processNext.Count > 0) {
			var currentVertex = processNext.Dequeue();
			topologicalSort.Add(currentVertex);
			foreach(var neighbor in graph.Vertices[currentVertex]) {
				incoming[neighbor.Key]--;
				if (incoming[neighbor.Key] == 0) {
					processNext.Enqueue(neighbor.Key);
				}
			}
		}

		return topologicalSort.Count == graph.Vertices.Count;
	}
}

public class DirectedGraph<T> where T : IEquatable<T> {
	public Dictionary<T, Dictionary<T, int>> Vertices { get; set; }

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
