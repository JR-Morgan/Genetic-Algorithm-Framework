using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace TSP
{
    /// <summary>
    /// This class encapsulates a graph of nodes
    /// </summary>
    public class Graph
    {
        public readonly List<Node> nodes;
        public Node StartNode => nodes.First();
        public int NodesCount => nodes.Count;
        public Graph(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        /// <summary>
        /// Parses a <see cref="Graph"/> from <paramref name="file"/> in a CSV format<br/>
        /// where column 1 is the Node id, column 2 is the X position, and column 3 is the Y position
        /// </summary>
        /// <example>
        /// Example of a correct CSV line is
        /// <code>1,38.24,20.42</code>
        /// </example>
        /// <param name="file">The file path of the file</param>
        /// <returns>A Graph</returns>
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

        /// <summary>
        /// Generates and returns a random <see cref="Graph"/>
        /// </summary>
        /// <param name="numberOfNodes">The number of <see cref="Node"/>s to be generated</param>
        /// <param name="bounds">The upper bounds for the <see cref="Node"/>s positions</param>
        /// <param name="seed">The random seed</param>
        /// <returns>The generated <see cref="Graph"/></returns>
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
