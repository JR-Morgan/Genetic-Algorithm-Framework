using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Travling_sales_problem
{
    class Program
    {
        private static Graph ParseGraphFromFile(string file)
        {
            var nodes = new List<Node>();
            StreamReader reader = File.OpenText(file);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("\""))
                {
                    string[] elements = line.Split(",");
                    nodes.Add(new Node(
                        id: int.Parse(elements[0]),
                        position: new Vector2(float.Parse(elements[1]),
                                              float.Parse(elements[2]))
                        ));

                }
            }

            return new Graph(nodes);
        }


        static void Main(string[] args)
        {
            string fileName = "graph.csv";

            Console.WriteLine($"Parsing graph from {fileName}");
            Graph graph = ParseGraphFromFile(fileName);

            Console.WriteLine(graph.Compute().ToString());


            Console.ReadLine();

        }

    }
}
