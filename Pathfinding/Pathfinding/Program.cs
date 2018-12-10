using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Pathfinding
{
    class Program
    {


        static void Main(string[] args)
        {

            //the file is the first argument after the file name

            string file = args[0];

            //get the data out of the file as an array
            int[] data = GetData(file);

            //number of nodes is at index 0
            int nodeNum = data[0];

            //n*2 indexes after the first are coordinates
            int coordRange = (nodeNum * 2);

            //organise a Dictionary of nodes with their respective coordinates and connections (hash table improves performance with larger numbers)
            Dictionary<int, Node> nodes = new Dictionary<int, Node>();
            IntialiseNodes(data, nodeNum, coordRange, nodes);

            //print values for connections and coordinates
            //DebugPrint(nodes);
            DebugFile(nodes, file);

            //run algorithm
            Calculate.AStar(nodes);

            //print answer to console
            AnswerPrint(Calculate.Answer.ToList());

            
        }

        private static void AnswerPrint(List<int> answerList)
        {
            Console.Write("\n");
            Console.Write("Answer: ");
            foreach (int id in answerList)
            {
                Console.Write(id + " ");
            }
            Console.Write("\n\n");

            Console.ReadKey();
        }

        private static void DebugFile(Dictionary<int, Node> nodes, string name)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"/datasets/" + name + "_debug.txt"))
            {

                file.WriteLine(name);
                file.WriteLine();
                for (int i = 1; i <= nodes.Count; i++)
                {
                    file.Write(string.Join(" ", nodes[i].Connections));
                    file.Write("\n");
                }
                file.WriteLine();

                for (int i = 1; i <= nodes.Count; i++)
                {
                    file.Write("Node " + nodes[i].ID + ", index " + i + ": " + nodes[i].X + "," + nodes[i].Y + " Connections: " + string.Join(" ", nodes[i].Connections));
                    file.Write("\n");
                }
            }
        }

        private static void DebugPrint(Dictionary<int, Node> nodes)
        {
            for (int i = 1; i <= nodes.Count; i++)
            {
                Console.Write(string.Join(" ", nodes[i].Connections));
                Console.Write("\n");
            }
            Console.WriteLine();

            for (int i = 1; i <= nodes.Count; i++)
            {
                Console.Write("Node " + nodes[i].ID + ", index " +  i + ": " +nodes[i].X + "," + nodes[i].Y + " Connections: " + string.Join(" ", nodes[i].Connections));
                Console.Write("\n");
            }
        }

        private static int[] GetData(string file)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"/datasets/" + file;
                string s = File.ReadAllText(path);
                string[] stringData = Regex.Split(s, ",");
                return Array.ConvertAll(stringData, int.Parse);
            }
            catch (Exception e)
            {
                Console.WriteLine("File cannot be read");
                Console.WriteLine(e.Message);
                Console.WriteLine(file);
                Environment.Exit(3);    //exit code 2 : The system cannot find the file specified.
                return null;
            }
        }

        private static void IntialiseNodes(int[] data, int nodeNum, int coordRange, Dictionary<int, Node> nodes)
        {
            
            int nodeID = 1;

            //create array of nodes with coordinates
            for (int i = 1; i < coordRange; i += 2)
            {
                Node node = new Node(nodeID, data[i], data[i + 1]);
                nodes.Add(nodeID, node);
                nodeID++;
            }

            //starting number of index to look at for connections from first node
            int consIndex = coordRange + 1;

            int from = 1;
            int to = 1;

            for (int i = 1; i < nodeNum * nodeNum; i++)
            {
                //after each segment of ints whoes length is equal to the number of caves we bump the "to" node and reset the from back to 1
                if (from > nodeNum)
                {
                    from = 1;
                    to++;
                }

                // if int at index is 1 then from is connected to to
                if (data[consIndex] == 1)
                {
                    if (from != to)
                    {
                        nodes[from].Connections.Add(to);
                    }
                }
                from++;
                consIndex++;
            }


            DebugPrint(nodes);

        }
    }
}
