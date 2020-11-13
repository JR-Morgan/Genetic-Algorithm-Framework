using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace TSP
{
    public class Graph
    {
        public readonly List<Node> nodes;
        public Node StartNode => nodes.First();
        public int NodesCount => nodes.Count;
        public Graph(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public static Graph ParseGraphFromFile(string file)
        {
            var nodes = new List<Node>();
            StreamReader reader = File.OpenText(file);
            string? line;

            var pattern = @"^\d+,-?\d+\.\d*,-?\d+\.\d*$";
            while ((line = reader.ReadLine()) != null)
            {
                if (Regex.IsMatch(line, pattern))
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

        public static Graph RandomGraph(uint numberOfNodes, Vector2 bounds, int seed)
        {
            Random random = new Random(seed);
            var nodes = new List<Node>();

            for(int i=0; i < numberOfNodes; i++)
            {
                nodes.Add(new Node(
                        id: i,
                        position: new Vector2((float)(random.NextDouble() * bounds.X), (float)(random.NextDouble() * bounds.Y))
                        ));
            }

            return new Graph(nodes);
        }
    }
}
