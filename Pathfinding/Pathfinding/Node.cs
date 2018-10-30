using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int[] Connections { get; private set; }

        public Node(int a, int b)
        {
            X = a;
            Y = b;
        }

        public void SetConnections(int[]x) { Connections = x; } 
    }
}
