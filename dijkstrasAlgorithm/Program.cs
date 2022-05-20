using System;
using System.Collections.Generic;
using System.Linq;

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
		Console.WriteLine(graph.AddEdge("a", "e", 2));
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

		Dictionary<string, string> previous;
		Dictionary<string, int> pathWeight;
		DijkstrasAlgorithm<string>(graph, "a", out previous, out pathWeight);
	}

	public static void DijkstrasAlgorithm<T>(DirectedGraph<T> graph, T startVertex, out Dictionary<T, T> previous, out Dictionary<T, int> pathWeight) where T : IEquatable<T> {
		previous = new Dictionary<T, T>(graph.Vertices.Count);
		pathWeight = new Dictionary<T, int>(graph.Vertices.Count);
		var priorityQueue = new PriorityQueue<int, T>(graph.Vertices.Count);
		foreach(var vertex in graph.Vertices) {
			previous.Add(vertex.Key, default(T));
			if (vertex.Key.Equals(startVertex)) {
				pathWeight.Add(vertex.Key, 0);
				priorityQueue.Add(0, vertex.Key);
			} else {
				pathWeight.Add(vertex.Key, int.MaxValue);
				priorityQueue.Add(int.MaxValue, vertex.Key);
			}
		}

		while (priorityQueue.Count > 0) {
			var current_priority_vertex = priorityQueue.RemoveMin();
			var shortestPathToCurrentVertex = current_priority_vertex.Item1;
			var currentVertex = current_priority_vertex.Item2;
			var currentVertexAdjacencyList = graph.Vertices[currentVertex];
			foreach(var outgoingEdge in currentVertexAdjacencyList) {
				var neighbor = outgoingEdge.Key;
				var edgeToNeighborWeight = outgoingEdge.Value;
				var previouslyFoundShortestPathToNeighbor = pathWeight[neighbor];
				var weightOfNewlyFoundPathToNeighbor = shortestPathToCurrentVertex + edgeToNeighborWeight;
				if (weightOfNewlyFoundPathToNeighbor < previouslyFoundShortestPathToNeighbor) {
					pathWeight[neighbor] = weightOfNewlyFoundPathToNeighbor;
					previous[neighbor] = currentVertex;
					priorityQueue.DecreasePriority(previouslyFoundShortestPathToNeighbor, neighbor, weightOfNewlyFoundPathToNeighbor);
				}
			}
		}
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

public class PriorityQueue<P, V> where P : IEquatable<P>, IComparable<P> {

	private HashSet<Tuple<P, V>> Set { get; set; }

	public int Count { get { return Set.Count; } }

	public PriorityQueue(int capacity) {
		Set = new HashSet<Tuple<P,V>>(capacity);
	}

	public bool Contains(P priority, V value) {
		return Set.Contains(new Tuple<P, V>(priority, value));
	}

	public bool Add(P priority, V value) {
		return Set.Add(new Tuple<P, V>(priority, value));
	}

	public Tuple<P,V> RemoveMin() {
		var toRemove = Set.MinBy(t => t.Item1);
		Set.Remove(toRemove);
		return toRemove;
	}

	public bool DecreasePriority(P originalPriority, V value, P newPriority) {
		if (!Set.Remove(new Tuple<P,V>(originalPriority, value))) {
			return false;
		}
		return Set.Add(new Tuple<P,V>(newPriority, value));
	}
}
