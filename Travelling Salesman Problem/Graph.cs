using System.Collections.Generic;
using System.Linq;

namespace TSP
{
    public class Graph
    {
        public readonly List<Node> nodes;
        public Node StartNode => nodes.First();

        public Graph(List<Node> nodes)
        {
            this.nodes = nodes;
        }

    }
}
