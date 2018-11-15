using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    public static class Calculate
    {
        public static IEnumerable<int> Answer = new List<int>();

        public static void AStar(Dictionary<int, Node> nodes)
        {
            //starting and ending points
            Node startNode = nodes[0];
            Node endNode = nodes[nodes.Count() - 1];

            //list of nodes already looked at : closed set
            List<Node> visited = new List<Node>();

            //set of available nodes not visited yet, begins with only the start node : open set
            List<Node> unvisited = new List<Node>() { nodes[0] };

            //dictionary of each node's ID and the node it can most efficiently be reached from
            Dictionary<int, Node> cameFrom = new Dictionary<int, Node>();

            //for each node the smallest cost to get to that node from the start
            Dictionary<int, double> costSoFar = new Dictionary<int, double>();

            //the cost to get to current node from the start. Start to start is 0
            double cost = 0;

            //first node's cost is completely heuristic
            startNode.Cost = Heuristic(cost, startNode, endNode);

            //answer in form of list of ints where ints are IDs of nodes

            while(unvisited.Count > 0)
            {
                Node currentNode = MinimumSearch(unvisited);

                if(currentNode == endNode)
                {
                    List<Node> answerNodes = TotalPath(currentNode, cameFrom);
                    Answer = answerNodes.Select(x => x.ID);
                }

                //update vistited/unvisited node lists
                unvisited.Remove(currentNode);
                visited.Add(currentNode);

                //for all nodes
                for(int i = 0; i<currentNode.Connections.Count(); i++)
                {
                    //check the flag at the index to see if it's connected
                    int flag = currentNode.Connections[i];

                    //if connected to the current node
                    if (flag == 1)
                    {
                        //neighbour node for comparisons
                        Node neighbour = nodes[i];

                        //ignore a neighbour if already evaluated
                        if (visited.Contains(neighbour))
                            continue;

                        //distance from start to neighbour
                        double tenativeCost = currentNode.Cost + NodeDistance(currentNode, neighbour);

                        if (!unvisited.Contains(neighbour)) //Discover a new node
                            unvisited.Add(neighbour);
                        else if (tenativeCost >= neighbour.Cost)
                            continue;   //this path s not the better one

                        //this path is the best for now, it is recorded. If the node has an index in the dictionary already, delete it and replace it
                        if (cameFrom.Keys.Contains(neighbour.ID))
                        {
                            cameFrom.Remove(neighbour.ID);
                        }
                        cameFrom.Add(neighbour.ID, currentNode);
                        neighbour.Cost = tenativeCost;
                        //If the node has an index in the dictionary already, delete it and replace it
                        if (cameFrom.Keys.Contains(neighbour.ID))
                        {
                            costSoFar.Remove(neighbour.ID);
                        }
                        costSoFar.Add(neighbour.ID, tenativeCost + Heuristic(tenativeCost, neighbour, endNode));
                    }
                }
            }

            Answer = Answer.Reverse();

            //Console.Write("Answer: ");
            //foreach (int id in Answer.ToList())
            //{
            //    Console.Write(id);
            //}
            //Console.Write("\n\n");

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
        public static List<Node> TotalPath(Node current, Dictionary<int, Node> cameFromDict)
        {
            //start with current node
            List<Node> total = new List<Node>() { current };
            //add the node that came before as long as there is a record of the node it came from in the dictionary
            while (cameFromDict.Keys.Contains(current.ID))
            {
                current = cameFromDict[current.ID];
                total.Add(current);
            }
            return total;
        }

        private static bool DestinationReached(Dictionary<int, Node> unvisited, Node destination)
        {
            foreach (KeyValuePair<int, Node> u in unvisited)
            {
                if (destination.Cost >= u.Value.Cost)
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
