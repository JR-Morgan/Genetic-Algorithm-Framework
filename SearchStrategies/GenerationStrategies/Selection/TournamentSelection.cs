﻿using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Selection
{
    public class TournamentSelection<S> : ISelectionStrategy<S>
    {
        private readonly Random random;


        private readonly uint k;
        private readonly uint selectionSize;

        public TournamentSelection(uint k, uint selectionSize, Random random)
        {
            this.k = k;
            this.selectionSize = selectionSize;
            this.random = random;
        }

        public IList<(S, int)> Select(IList<S> population, ICostFunction<S> fitness)
        {

            (S, int)[] selection = new (S, int)[selectionSize];


            for (int i = 0; i < selectionSize; i++) //For each Round
            {
                List<S> workingPopulation = new List<S>(population);
                S? bestCandidate = default;
                int bestCandidateIndex = default;

                for (int j = 0; j < k; j++)
                {
                    int index = random.Next(workingPopulation.Count);
                    S candidate = workingPopulation[index];
                    workingPopulation.Remove(candidate);

                    if (bestCandidate == null
                        || fitness.Cost(candidate) < fitness.Cost(bestCandidate))
                    {
                        bestCandidate = candidate;
                        bestCandidateIndex = index;
                    }

                }

                if (bestCandidate == null) throw new Exception();
                
                selection[i] = (bestCandidate, bestCandidateIndex);

            }


            return selection;

        }

    }
}
