using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    class GraphParser
    {
        public Graph ParseAdjacenciesArray(string input)
        {
            string[] separator = {Environment.NewLine};
            var lines = input.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)).ToList();
            var k = int.Parse(lines[0][0]);
            var l = int.Parse(lines[0][1]);
            var n = int.Parse(lines[1][0]);
            var gLine = lines.Skip(2).ToList();
            var graphLine = new List<string>();
            foreach (var line in gLine)
            {
                foreach (var e in line)
                {
                    graphLine.Add(e);
                }
            }

            var adjArr = graphLine.Select(int.Parse).ToList();
            var nodes = new List<Node>();
            var graph = new Graph();

            for (var i = 0; i < k + l; i++)
            {
                var node = new Node(i);
                if (i < l)
                    graph.YNodes.Add(node);
                else
                    graph.XNodes.Add(node);


                nodes.Add(node);
                graph.AddNode(node);
            }

            var index = 0;


            while (adjArr[adjArr[index] - 1] != 32767)
            {
                var nextIndex = adjArr[index + 1];
                var x = 2;
                while (nextIndex == 0)
                {
                    nextIndex = adjArr[index + x];
                }

                var currIndex = adjArr[index];
                for (var i = currIndex - 1; i < nextIndex - 1; i++)
                {
                    if (adjArr[i] == 32767)
                    {
                        break;
                    }

                    graph.AttachNode(nodes[index + l], nodes[adjArr[i] - 1], 1);
                }

                index++;

                while (adjArr[index] == 0)
                {
                    index++;
                }
            }

            return graph;
        }

        public string SerializeMatching(List<Tuple<Node, Node>> matching, Graph graph)
        {
            var sb = new StringBuilder();
            var ordered = matching.OrderBy(x => x.Item1.Number);
            var res = new int[graph.XNodes.Count];


            foreach (var pair in ordered)
            {
                res[pair.Item1.Number - graph.YNodes.Count] = pair.Item2.Number + 1;
            }

            foreach (var x in res)
            {
                if (x != 0)
                    sb.Append(x);
                else
                {
                    sb.Append(0);
                }

                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}