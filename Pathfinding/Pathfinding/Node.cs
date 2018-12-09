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
        public List<int> Connections { get; set; }
        public double Cost { get; set; }
        public bool Visited { get; set; }

        public Node(int id, int a, int b)
        {
            ID = id;
            X = a;
            Y = b;

            //by default is unvisited and tentative distance is infinity
            Visited = false;
            Cost = Double.PositiveInfinity;

            //connections initialised
            Connections = new List<int>();
        }

        //public void SetConnections(int[] con) { Connections = con; } 
    }
}
