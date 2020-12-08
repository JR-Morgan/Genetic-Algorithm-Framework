using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Selection
{
    class TournamentSelection<S> : ISelectionStrategy<S>
    {
        private static Random random = new Random(); //TODO


        private readonly uint k;
        private readonly uint selectionSize;

        public TournamentSelection(uint k, uint selectionSize)
        {
            this.k = k;
            this.selectionSize = selectionSize;
        }

        public S[] Select(S[] population, ICostFunction<S> fitness)
        {

            S[] selection = new S[selectionSize];


            for (int i = 0; i < selectionSize; i++) //For each Round
            {
                List<S> workingPopulation = new List<S>(population);
                S? bestCandidate = default;

                for (int j = 0; j < k; j++)
                {
                    S candidate = workingPopulation[random.Next(workingPopulation.Count)];
                    workingPopulation.Remove(candidate);

                    bestCandidate = bestCandidate == null ? candidate : fitness.FittestP(candidate, bestCandidate );

                }

                if (bestCandidate == null) throw new Exception();

                selection[i] = bestCandidate;

            }


            return selection;

        }

    }
}
