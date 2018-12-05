using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    public class Node
    {
        public int Number;

        public Node(int number)
        {
            Number = number;
        }

        public static bool operator ==(Node second, Node other)
        {
            if (other is null)
                return false;
            return second.Number == other.Number;
        }

        public static bool operator !=(Node second, Node other)
        {
            return !(second == other);
        }
    }
}