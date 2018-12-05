using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace MaxMatching
{
    public static class GraphAlgorithms
    {
        public static List<Tuple<Node, Node>> HK_GetMaxMatching(Graph graph)
        {
            var maxMatching = new List<Tuple<Node, Node>>();
            var chain = GetChain(graph);
            var maxFlow = FF_GetMaxFlow(chain);
            var fict = new HashSet<Node>() {graph.SNode, graph.TNode};
            foreach (var e in maxFlow)
            {
                if (e.Value > 0 && !fict.Contains(e.Key.Item1) && !fict.Contains(e.Key.Item2))
                {
                    maxMatching.Add(e.Key);
                }
            }

            return maxMatching;
        }

        private static Dictionary<Tuple<Node, Node>, int> FF_GetMaxFlow(Graph chain)
        {
            var flow = new Dictionary<Tuple<Node, Node>, int>();
            foreach (var edge in chain.Edges)
            {
                flow[Tuple.Create(edge.FirstNode, edge.SecondNode)] = 0;
                flow[Tuple.Create(edge.SecondNode, edge.FirstNode)] = 0;
            }

            var path = GetPath(chain, flow);
            while (path != null)
            {
                var cf = path.Min(x => chain.GetWeight(x.Item1, x.Item2) - flow[x]);

                foreach (var edge in path)
                {
                    flow[Tuple.Create(edge.Item1, edge.Item2)] += cf;
                    flow[Tuple.Create(edge.Item2, edge.Item1)] -= cf;
                }

                path = GetPath(chain, flow);
            }

            return flow;
        }

        private static List<Tuple<Node, Node>> GetPath(Graph graph, Dictionary<Tuple<Node, Node>, int> currentFlow)
        {
            var visited = new HashSet<Node>();

            var visitedFrom = new Dictionary<Node, Node>();

            visitedFrom[graph.SNode] = null;
            visited.Add(graph.SNode);
            var nodes = new Queue<Node>();
            nodes.Enqueue(graph.SNode);
            var currentPath = new List<Tuple<Node, Node>>();

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();
                foreach (var node in graph.nodeToNodes[currentNode])
                {
                    if (!visited.Contains(node) && graph.GetWeight(currentNode, node) -
                        currentFlow[Tuple.Create(currentNode, node)] > 0)
                    {
                        visitedFrom[node] = currentNode;
                        visited.Add(node);
                        nodes.Enqueue(node);
                        if (node == graph.TNode)
                        {
                            var cNode = graph.TNode;
                            while (!(visitedFrom[cNode] is null))
                            {
                                currentPath.Add(Tuple.Create(visitedFrom[cNode], cNode));
                                cNode = visitedFrom[cNode];
                            }

                            currentPath.Reverse();
                            return currentPath;
                        }
                    }
                }
            }

            return null;
        }

        private static List<Tuple<Node, Node>> AddNode(Node node, Graph graph, List<Tuple<Node, Node>> path,
            Dictionary<Tuple<Node, Node>, int> currentFlow, HashSet<Node> visited)
        {
            foreach (var incidentNode in graph.nodeToNodes[node])
            {
                if (visited.Contains(incidentNode))
                    continue;
                var nVisited = new HashSet<Node>();
                nVisited.UnionWith(visited);
                nVisited.Add(incidentNode);

                var c = graph.GetWeight(node, incidentNode);
                var cf = c - currentFlow[Tuple.Create(node, incidentNode)];
                if (cf > 0)
                {
                    var currentPath = path.ToList();
                    currentPath.Add(Tuple.Create(node, incidentNode));
                    var p = AddNode(incidentNode, graph, currentPath, currentFlow, nVisited);
                    if (p == null)
                        continue;
                    if (p.Last().Item2 == graph.TNode)
                        return p;
                }
            }

            return null;
        }

        private static Graph GetChain(Graph graph)
        {
            graph.SNode = new Node(-1);
            graph.TNode = new Node(-2);

            foreach (var node in graph.XNodes)
            {
                graph.AttachNode(graph.SNode, node, 1);
            }

            foreach (var node in graph.YNodes)
            {
                graph.AttachNode(node, graph.TNode, 1);
            }

            return graph;
        }

        public static void test()
        {
            var maxMatching = new List<Tuple<Node, Node>>();
            var g = new Graph();
            var nodes = new List<Node>();
            for (var i = 0; i < 6; i++)
            {
                nodes.Add(new Node(i));
            }

            g.XNodes = new HashSet<Node> {nodes[0], nodes[1], nodes[2]};
            g.YNodes = new HashSet<Node> {nodes[3], nodes[4], nodes[5]};
            g.AttachNode(nodes[0], nodes[3], 1);
            g.AttachNode(nodes[0], nodes[4], 1);
            g.AttachNode(nodes[1], nodes[3], 1);
            g.AttachNode(nodes[1], nodes[4], 1);
            g.AttachNode(nodes[2], nodes[5], 1);
            var ch = GetChain(g);
            var maxFlow = FF_GetMaxFlow(ch);
            var fict = new HashSet<Node>() {ch.SNode, ch.TNode};
            foreach (var e in maxFlow)
            {
                if (e.Value > 0 && !fict.Contains(e.Key.Item1) && !fict.Contains(e.Key.Item2))
                {
                    maxMatching.Add(e.Key);
                }
            }

            var result = maxMatching;
        }
    }
}