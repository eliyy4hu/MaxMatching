using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    public class Edge
    {
        public Node FirstNode;
        public Node SecondNode;
        public int Weight;

        public Edge(Node firstNode, Node secondNode, int weight)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            Weight = weight;
        }
    }
}