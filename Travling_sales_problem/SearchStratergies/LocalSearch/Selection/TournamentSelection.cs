using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction;
using Travling_sales_problem.SearchStratergies.LocalSearch.StepFunctions;

//genetic search algorithm for graph traversal optimisation

namespace Travling_sales_problem.SearchStratergies.LocalSearch.Selection
{
    class TournamentSelection : ISelectionStrategy
    {
        private static Random random = new Random();



        private readonly uint k;
        public TournamentSelection(uint k)
        {
            this.k = k;
        }

        public Route[] Select(IEnumerable<Route> population, uint selectionSize, IStepFunction step)
        {
            List<Route> workingPopulation = new List<Route>(population);

            Route[] selection = new Route[selectionSize];


            for (int i = 0; i < selectionSize; i++) //For each Round
            {
                
                Route? bestCandidate = null;

                for (int j = 0; j < k; j++)
                {
                    Route candidate = workingPopulation[random.Next(workingPopulation.Count + 1)];
                    workingPopulation.Remove(candidate);

                    bestCandidate = bestCandidate == null ? candidate : step.StepP(new Route[] { candidate, bestCandidate });
                }



                if (bestCandidate == null) throw new Exception();
                selection[i] = bestCandidate;

            }

            




            return selection;

        }
    }
}
