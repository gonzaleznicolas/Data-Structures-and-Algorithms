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

		Console.WriteLine();

		foreach(var v in graph.Dfs("a"))
			Console.WriteLine(v);
		
		Console.WriteLine();

		foreach(var v in graph.Bfs("a"))
			Console.WriteLine(v);
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
		Dictionary<T, int> aAdjacencyDict;
		Dictionary<T, int> bAdjacencyDict;
		if (!Vertices.TryGetValue(a, out aAdjacencyDict)
			|| !Vertices.TryGetValue(b, out bAdjacencyDict)
			|| aAdjacencyDict.ContainsKey(b)
			|| bAdjacencyDict.ContainsKey(a)) {
			return false;
		}

		aAdjacencyDict.Add(b, weight);
		bAdjacencyDict.Add(a, weight);
		return true;
	}

	public IEnumerable<T> Dfs(T startingVertex) {
		if (!Vertices.ContainsKey(startingVertex)) {
			throw new ArgumentException("The specified vertex does not exist.");
		}

		var visited = new HashSet<T>(Vertices.Count);
		foreach(var v in DfsHelper(startingVertex, visited))
			yield return v;
	}

	// assumes v exists
	private IEnumerable<T> DfsHelper(T vertex, HashSet<T> visited) {
		yield return vertex;
		visited.Add(vertex);
		var vAdjacencyDict = Vertices[vertex];
		foreach(var kvp in vAdjacencyDict) {
			if (!visited.Contains(kvp.Key)) {
				foreach(var v in DfsHelper(kvp.Key, visited))
					yield return v;
			}
		}
	}

	public IEnumerable<T> Bfs(T startingVertex) {
		if (!Vertices.ContainsKey(startingVertex)) {
			throw new ArgumentException("The specified vertex does not exist.");
		}

		var addedToQueue = new HashSet<T>(Vertices.Count);
		var queue = new Queue<T>(Vertices.Count);
		addedToQueue.Add(startingVertex);
		queue.Enqueue(startingVertex);
		while (queue.Count > 0) {
			var vertex = queue.Dequeue();
			yield return vertex;
			var vertexAdjacencyList = Vertices[vertex];
			foreach(var kvp in vertexAdjacencyList) {
				if (!addedToQueue.Contains(kvp.Key)) {
					addedToQueue.Add(kvp.Key);
					queue.Enqueue(kvp.Key);
				}
			}
		}
	}
}
