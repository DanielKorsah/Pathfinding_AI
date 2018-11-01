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

        public static void Dijkstra(Dictionary<int, Node> nodes)
        {
            //make set of unvisited nodes
            Dictionary<int, Node> unvisited = nodes;

            Node start = nodes[0];
            Node destination = nodes[nodes.Count - 1];
            Node currentNode;

            start.Distance = 0;
            destination.Visited = true;

            int currentNodeID = 1;
            currentNode = nodes[currentNodeID - 1];

            //while the destination smaller than all unvisited or all nodes visited
            while (unvisited.Count != 0 || DestinationReached(unvisited, destination))
            {
                for (int i = 0; i < currentNode.Connections.Length; i++)
                {
                    //if connected
                    if (currentNode.Connections[i] == 1)
                    {
                        //the node being looked at
                        Node comparisonNode = nodes[currentNode.Connections[i]];

                        //consider only unvisited neighbours
                        if (!comparisonNode.Visited)
                        {
                            //calculate distance to node
                            double distance = NodeDistance(currentNode, comparisonNode);

                            //set distance to node if it's smaller than previous distance
                            if (distance < comparisonNode.Distance)
                                comparisonNode.Distance = distance;
                        }
                    }
                }

                //mark this node as visited
                currentNode.Visited = true;
                unvisited.Remove(currentNodeID - 1);

                //track shortest distance withing node
                int shortest = int.MaxValue;

                //select the node with the smallest distance
                for (int i = 0; i < currentNode.Connections.Length; i++)
                {
                    if (currentNode.Connections[i] == 1)
                    {
                        Node comparisonNode = nodes[currentNode.Connections[i]];
                        if (comparisonNode.Distance < shortest)
                            shortest = comparisonNode.ID-1;
                    }
                }

                //set the new current node to the one with the shortest path
                currentNode = nodes[shortest];
            }

            Answer.Clear();
        }

        private static bool DestinationReached(Dictionary<int, Node> unvisited, Node destination)
        {
            foreach (KeyValuePair<int, Node> u in unvisited)
            {
                if (destination.Distance >= u.Value.Distance)
                    return false;
            }
            return true;
        }

        private static double NodeDistance(Node currentNode, Node comparisonNode)
        {
            double a = comparisonNode.X - currentNode.X;
            double b = comparisonNode.Y - currentNode.Y;
            double c = Math.Sqrt((a * a) + (b * b));
            return c;
        }

    }
}
