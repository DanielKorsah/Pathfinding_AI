using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pathfinding
{
    public static class IO
    {
        public static void AnswerPrint(List<int> answerList)
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

        public static void DebugFile(Dictionary<int, Node> nodes, string name)
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

        public static void OutputFile(Dictionary<int, Node> nodes, string name)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"/datasets/" + name + ".csn"))
            {
                for(int i = 0; i < Calculate.Answer.ToList().Count(); i++)
                {
                    //space delimiter printed after first element
                    if (i != 0)
                        file.Write(" ");
                    file.Write(Calculate.Answer.ToList()[i]);
                }
                
            }
        }

        public static void DebugPrint(Dictionary<int, Node> nodes)
        {
            for (int i = 1; i <= nodes.Count; i++)
            {
                Console.Write(string.Join(" ", nodes[i].Connections));
                Console.Write("\n");
            }
            Console.WriteLine();

            for (int i = 1; i <= nodes.Count; i++)
            {
                Console.Write("Node " + nodes[i].ID + ", index " + i + ": " + nodes[i].X + "," + nodes[i].Y + " Connections: " + string.Join(" ", nodes[i].Connections));
                Console.Write("\n");
            }
        }
    }
}
