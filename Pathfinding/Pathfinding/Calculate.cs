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

        public static void AStar(Dictionary<int, Node> nodes)
        {
            //starting and ending points
            Node startNode = nodes[0];
            Node endNode = nodes[nodes.Count() - 1];

            //list of nodes already looked at
            List<Node> visited = new List<Node>();

            //set of available nodes not visited yet, begins with only the start node
            List<Node> unvisited = new List<Node>() { nodes[0] };

            //dictionary of each node's ID and the node it can most efficiently be reached from
            Dictionary<int, Node> cameFrom;

            //for each node the smallest cost to get to that node from the start
            Dictionary<int, double> costSoFar;

            //the cost to get to current node from the start. Start to start is 0
            double cost = 0;


            cost = Heuristic(cost, startNode, endNode);

            while(unvisited.Count > 0)
            {
                Node currentNode = MinimumSearch(unvisited);

                if(currentNode == endNode)
                {
                    
                }
            }
        }

        //Heuristic is euclidian distance from point to end plus cost of getting here
        private static double Heuristic(double currentCost, Node thisNode, Node endNode)
        {
            double distanceToEnd = NodeDistance(thisNode, endNode);
            return currentCost + distanceToEnd;
        }

        //get the available node with the lowest cost
        private static Node MinimumSearch(List<Node> nodes)
        {
            Node min = nodes[0];
            foreach( Node n in nodes)
            {
                if(n.Cost < min.Cost)
                min = n;
            }
            return min;
        }

        //path of nodes
        public static List<Node> TotalPath(Node current, Dictionary<int, Node> cameFrom)
        {
            //start with current node
            List<Node> total = new List<Node>() { current };
            //add the node that came before as long as there is a record of the node it came from in the dictionary
            while (cameFrom.Keys.Contains(current.ID))
            {
                current = cameFrom[current.ID];
                total.Add(current);
            }
            return total;
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
