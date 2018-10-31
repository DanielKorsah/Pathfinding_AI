using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    public static class Calculate
    {
        public static List<Node> Answer = new List<Node>();

        public static void Dijkstra(List<Node> nodes)
        {
            Node start = nodes[0];
            Node destination = nodes[nodes.Count-1];


            Answer.Clear();
        }
    }
}
