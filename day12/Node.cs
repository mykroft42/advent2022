using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day12
{
    internal class Node
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char height
        {
            get
            {
                if (RealHeight == 'S') return 'a';
                else if (RealHeight == 'E') return 'z';
                else return RealHeight;
            }
            set
            {
                RealHeight = value;
            }
        }

        public char RealHeight { get; set; }

        public List<Node> Neighbors { get; set; } = new List<Node>();
    }
}
