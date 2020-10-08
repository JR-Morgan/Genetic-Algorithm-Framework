using System;
using System.Collections.Generic;
using System.Linq;
using Travling_sales_problem.Solution_Stratergies;

namespace Travling_sales_problem
{
    class Graph
    {
        public readonly List<Node> nodes;
        public Node StartNode => nodes.First();

        public Graph(List<Node> nodes)
        {
            this.nodes = nodes;
        }


        public Log Compute(ISolution solutionStratergy)
        {
            return solutionStratergy.Compute(this);
        }

    }
}
