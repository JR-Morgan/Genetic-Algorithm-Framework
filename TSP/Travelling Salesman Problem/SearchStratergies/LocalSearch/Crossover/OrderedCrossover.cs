using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.SearchStratergies.LocalSearch.Crossover
{
    class OrderedCrossover : ICrossover
    {
        private static Random random = new Random();

        public Route CrossOver(Route parent1, Route parent2)
        {
            List<Node> ChildP1 = new List<Node>();
            

            int geneA = random.Next(parent1.RouteNodes.Count);
            int geneB = random.Next(parent1.RouteNodes.Count);

            int startGene = Math.Min(geneA, geneB);
            int endGene = Math.Max(geneA, geneB);

            for(int i = startGene; i< endGene; i++)
            {
                ChildP1.Add(parent1.RouteNodes[i]);
            }

            List<Node> ChildP2 = new List<Node>();
            foreach (Node node in parent2.RouteNodes)
            {
                if (!ChildP1.Contains(node)) ChildP2.Add(node);
            }
                

            ChildP1.AddRange(ChildP2);

            return new Route(ChildP1);

        }
    }
}
