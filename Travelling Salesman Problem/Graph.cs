using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
