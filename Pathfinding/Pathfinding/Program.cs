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

        static int[] getData()
        {
            try
            {
                StreamReader s = new StreamReader("/datasets/" + "file");
                string[] stringData = Regex.Split(s.ReadToEnd(), ",");
                return Array.ConvertAll(stringData, int.Parse);
            }
            catch (Exception e)
            {
                Console.WriteLine("File cannot be read");
                Console.WriteLine(e.Message);
                Environment.Exit(3);    //exit code 2 : The system cannot find the file specified.
                return null;
            }
        }

        static void Main(string[] args)
        {
            //the file is the first argument after the file name
            string file = args[1];

            //get the data out of the file as an array
            int[] data = getData();

            //number of nodes is at index 0
            int nodeNum = data[0];

            //n*2 indexes after the first are coordinates
            int coordRange = (nodeNum * 2) + 1;

            //create array of nodes with coordinates
            for(int i = 1; i>coordRange; i+=2)
            {
                Node node = new Node(i, i + 1);
            }
        }
    }
}
