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

        static int[] getData(string file)
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
        

        static void Main(string[] args)
        {
            
            //the file is the first argument after the file name
            
            string file = args[0];

            //get the data out of the file as an array
            int[] data = getData(file);

            //number of nodes is at index 0
            int nodeNum = data[0];

            //n*2 indexes after the first are coordinates
            int coordRange = (nodeNum * 2);

            List<Node> nodes = new List<Node>();
            //create array of nodes with coordinates
            for(int i = 1; i < coordRange; i+=2)
            {
                Node node = new Node(i, i + 1);
                nodes.Add(node);
            }

            //starting number of index to look at for connections from first node
            int consIndex = coordRange + 1;

            //set connections for each node
            for (int i = 0; i<nodeNum; i++)
            {

                int[] temp = new int[nodeNum];

                //copy from data array starting at index where connections start, to the temp array at index 0 for length of the number of nodes
                Array.Copy(data, consIndex, temp, 0, nodeNum);

                nodes[i].SetConnections(temp);

                //set the next starting index for array copy
                consIndex += nodeNum;
            }

            foreach(Node n in nodes)
            {
                Console.Write(string.Join(" ", n.Connections));
                Console.Write("\n");
            }

            Console.ReadKey();
        }
    }
}
