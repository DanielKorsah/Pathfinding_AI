﻿using System;
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

            //run algorithm
            Calculate.Dijkstra(nodes);

            //print values for connections and coordinates
            DebugPrint(nodes);
        }

        private static void DebugPrint(Dictionary<int, Node> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Console.Write(string.Join(" ", nodes[i].Connections));
                Console.Write("\n");
            }
            Console.WriteLine();

            for (int i = 0; i < nodes.Count; i++)
            {
                Console.Write(string.Join(" ", i + ": " +nodes[i].X + "," + nodes[i].Y));
                Console.Write("\n");
            }

            Console.ReadKey();
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
                nodes.Add(nodeID-1, node);
                nodeID++;
            }

            //starting number of index to look at for connections from first node
            int consIndex = coordRange + 1;

            //set connections for each node
            for (int i = 0; i < nodeNum; i++)
            {

                int[] temp = new int[nodeNum];

                //copy from data array starting at index where connections start, to the temp array at index 0 for length of the number of nodes
                Array.Copy(data, consIndex, temp, 0, nodeNum);

                nodes[i].SetConnections(temp);

                //set the next starting index for array copy
                consIndex += nodeNum;
            }
        }
    }
}
