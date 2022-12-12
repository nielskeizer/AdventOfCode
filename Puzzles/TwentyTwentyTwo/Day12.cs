using System.Collections;
using System.Text;

namespace Puzzles.TwentyTwentyTwo
{
    public class Day12 : IPuzzle
    {
        public string SolveFirst(string[] input)
        {
            var graph = CreateGraph(input, (vertex, neighborCandidate) => neighborCandidate.Height - vertex.Height <= 1);
            var startNode = graph.Nodes.Single(x => x.Type == 'S');
            var endNode = graph.Nodes.Single(x => x.Type == 'E');
            return graph.ShortestDistance(startNode, endNode)?.ToString() ?? "Not found.";
        }

        public string SolveSecond(string[] input)
        {
            var graph = CreateGraph(input, (vertex, neighborCandidate) => vertex.Height - neighborCandidate.Height <= 1);
            var startNode = graph.Nodes.Single(x => x.Type == 'E');

            return graph.ShortestDistanceToLowestPoint(startNode).ToString() ?? "Not found.";
        }

        Graph CreateGraph(string[] input, Func<Node,Node, bool> isEdge)
        {
            var graph = new Graph();
            foreach (var (rowIndex, row) in input.Select((item, index) => (index, item)))
            {
                foreach (var (columnIndex, height) in row.Select((item, index) => (index, item)))
                {
                    graph.AddNode(height, (columnIndex, rowIndex));
                }
            }

            foreach (var node in graph.Nodes)
            {
                var test = (int x, int y) => graph.Contains((x, y)) && isEdge(node, graph.FindNode((x, y))); // (graph.FindNode((x, y)).Height - node.Height <= 1);

                var (x, y) = node.Position;
                if (test(x - 1, y))
                {
                    graph.AddEdge((x, y), (x - 1, y));
                }
                if (test(x + 1, y))
                {
                    graph.AddEdge((x, y), (x + 1, y));
                }
                if (test(x, y - 1))
                {
                    graph.AddEdge((x, y), (x, y - 1));
                }
                if (test(x, y + 1))
                {
                    graph.AddEdge((x, y), (x, y + 1));
                }
            }

            return graph;
        }

        class Node
        {
            public char Height { get; }
            public char Type { get; }
            public (int X, int Y) Position { get; }
            public List<Node> Neighbors { get; set; } = new();

            public Node(char height, (int X, int Y) position)
            {
                if (height == 'S')
                {
                    Height = 'a';
                    Type = 'S';
                }
                else if (height == 'E')
                {
                    Height = 'z';
                    Type = 'E';
                }
                else
                {
                    Height = height;
                    Type = 'r';
                }
                Position = position;
            }
        }

        class Graph
        {
            public IEnumerable<Node> Nodes => nodes.Values;
            Dictionary<(int X, int Y), Node> nodes = new();

            public void AddNode(char height, (int X, int Y) position)
            {
                nodes.Add(position, new Node(height, position));
            }

            public void AddEdge(Node fromNode, Node toNode)
            {
                fromNode.Neighbors.Add(toNode);
            }

            public void AddEdge((int X, int Y) from, (int X, int Y) to)
            {
                AddEdge(nodes[from], nodes[to]);
            }
            public Node FindNode((int X, int Y) position)
            {
                return nodes[position];
            }
            public bool Contains((int X, int Y) position)
            {
                return nodes.ContainsKey(position);
            }

            public int? ShortestDistance(Node from, Node to)
            {
                var (distances, previousNodes) = Dijkstra(from);
                return distances[to];
            }

            public int? ShortestDistanceToLowestPoint(Node from)
            {
                var (distances, previousNodes) = Dijkstra(from);
                return Nodes.Where(x => x.Height == 'a').Select(x => distances[x]).Min();
            }

            (Dictionary<Node, int?>, Dictionary<Node, Node?>) Dijkstra(Node from)
            {
                var distances = Nodes.ToDictionary(x => x, x => (int?)null);
                var previousNodes = Nodes.ToDictionary(x => x, x => (Node?)null);

                distances[from] = 0;
                var queue = Nodes.ToList();
                while (queue.Count() > 0)
                {
                    var node = queue.Where(x => distances[x] is not null).OrderBy(x => distances[x]).FirstOrDefault();
                    if (node is null) break;
                    queue.Remove(node);
                    foreach (var neighbor in node.Neighbors.Where(x => queue.Contains(x)))
                    {
                        var newDistance = distances[node] + 1;
                        if (newDistance < distances[neighbor] || distances[neighbor] is null)
                        {
                            distances[neighbor] = newDistance;
                            previousNodes[neighbor] = node;
                        }
                    }
                }

                return (distances, previousNodes);
            }
        }
    }
}