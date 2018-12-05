using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    public class Graph
    {
        public HashSet<Node> XNodes = new HashSet<Node>();
        public HashSet<Node> YNodes = new HashSet<Node>();
        public Node SNode;
        public Node TNode;

        public HashSet<Node> Nodes = new HashSet<Node>();
        public HashSet<Edge> Edges = new HashSet<Edge>();
        public Dictionary<Node, HashSet<Node>> nodeToNodes = new Dictionary<Node, HashSet<Node>>();
        public Dictionary<Node, HashSet<Edge>> nodeToEdges = new Dictionary<Node, HashSet<Edge>>();


        public void AddNode(Node node)
        {
            Nodes.Add(node);
            if (!nodeToNodes.ContainsKey(node))
                nodeToNodes[node] = new HashSet<Node>();
            if (!nodeToEdges.ContainsKey(node))
                nodeToEdges[node] = new HashSet<Edge>();
        }

        public int GetWeight(Node node1, Node node2)
        {
            var edge = nodeToEdges[node1].First(x => x.FirstNode == node2 || x.SecondNode == node2);
            if (edge.FirstNode == node1)
                return edge.Weight;
            return 0;
        }

        public void AttachNode(Node node, Node nodeToAttach, int weight = 0)
        {
            AddNode(node);
            AddNode(nodeToAttach);
            if (nodeToNodes.ContainsKey(node) && nodeToNodes[node].Contains(nodeToAttach))
            {
                return;
            }

            nodeToNodes[node].Add(nodeToAttach);
            nodeToNodes[nodeToAttach].Add(node);
            var edge = new Edge(node, nodeToAttach, weight);
            Edges.Add(edge);
            nodeToEdges[node].Add(edge);
            nodeToEdges[nodeToAttach].Add(edge);
        }
    }
}