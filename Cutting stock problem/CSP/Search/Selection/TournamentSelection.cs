using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.Search.Selection
{
    class TournamentSelection : ISelectionStrategy<ISolution>
    {
        private static Random random = new Random(); //TODO


        private readonly uint k;
        public TournamentSelection(uint k)
        {
            this.k = k;
        }

        public ISolution[] Select(IEnumerable<ISolution> population, int selectionSize, IStepFunction<ISolution> step)
        {


            ISolution[] selection = new ISolution[selectionSize];


            for (int i = 0; i < selectionSize; i++) //For each Round
            {
                List<ISolution> workingPopulation = new List<ISolution>(population);
                ISolution? bestCandidate = null;

                for (int j = 0; j < k; j++)
                {
                    ISolution candidate = workingPopulation[random.Next(workingPopulation.Count)];
                    workingPopulation.Remove(candidate);

                    bestCandidate = bestCandidate == null ? candidate : step.FittestP(new ISolution[] { candidate, bestCandidate });

                }



                if (bestCandidate == null) throw new Exception();

                selection[i] = bestCandidate;

            }


            return selection;

        }

    }
}
