using CSP.Search.Initialisation;
using SearchExtensions;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Crossover
{
    class CrossoverFunction : IGenerationOperation<ISolution>
    {
        private static readonly Random random = new Random(); //TODO

        private readonly ISelectionStrategy<ISolution> selectionStrategy;
        private readonly InitalisationStrategy repairStrategy;

        private readonly float elitismProportion, selectionProportion;

        public CrossoverFunction(ISelectionStrategy<ISolution> selectionStrategy, InitalisationStrategy repairStrategy, float elitism = 0f, float selectionSize = 0.5f)
        {
            this.selectionStrategy = selectionStrategy;
            this.repairStrategy = repairStrategy;
            this.elitismProportion = elitism;
            this.selectionProportion = selectionSize;
        }


        public ISolution[] Operate(ISolution[] population, IFitnessFunction<ISolution> fitnessFunction)
        {
            int ToCount(float proportion) => (int)Math.Ceiling(population.Length * proportion);
            int elitism = ToCount(elitismProportion);
            int selection = ToCount(selectionProportion);


            ISolution[] parents = selectionStrategy.Select(population, selection, fitnessFunction);

            ISolution[] children = new ISolution[parents.Length];

            ISolution[] pool = (ISolution[])parents.Clone();
            pool.Shuffle(random);

            
            for (int i = 0; i < elitism; i++)
            {
                children[i] = parents[i];
            }

            for (int i = elitism; i < parents.Length; i++)
            {
                children[i] = Crossover(parents[i], pool[parents.Length - i - 1]);
                repairStrategy.Repair(children[i]);
            }

            return children;

        }



        public ISolution Crossover(ISolution parent1, ISolution parent2)
        {
            parent1 = parent1.Copy();
            parent2 = parent2.Copy();

            int geneA = random.Next(parent1.Activities.Count);
            int geneB = random.Next(parent1.Activities.Count);

            int startGene = Math.Min(geneA, geneB);
            int endGene = Math.Max(geneA, geneB);
         
            return Crossover(parent1, parent2, startGene , endGene);
        }

        private ISolution Crossover(ISolution parent1, ISolution parent2, int start, int end)
        {
            List<Activity> ChildP1 = new List<Activity>();

            for (int i = start; i < end; i++)
            {
                ChildP1.Add(parent1.Activities[i]);
            }

            List<Activity> ChildP2 = new List<Activity>();
            foreach (Activity node in parent2.Activities)
            {
                if (!ChildP1.Contains(node)) ChildP2.Add(node);
            }


            ChildP1.AddRange(ChildP2);

            return new Solution(parent1.Problem, ChildP1);
        }

        
    }
}
