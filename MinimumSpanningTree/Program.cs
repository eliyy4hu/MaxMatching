using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new Reader();
            var input = reader.Read();
            var parser = new GraphParser();
            var graph = parser.ParseAdjacenciesArray(input);
            
            var maxMatching = GraphAlgorithms.HK_GetMaxMatching(graph);
            
            var res = parser.SerializeMatching(maxMatching, graph);
            reader.Write(res);
        }
    }
}