using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    public class Node
    {
        public int ID { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int[] Connections { get; private set; }
        public double Distance { get; set; }
        public bool Visited { get; set; }

        public Node(int id, int a, int b)
        {
            ID = id;
            X = a;
            Y = b;

            //by default is unvisited and tentative distance is infinity
            Visited = false;
            Distance = Double.PositiveInfinity;
        }

        public void SetConnections(int[] con) { Connections = con; } 
    }
}
