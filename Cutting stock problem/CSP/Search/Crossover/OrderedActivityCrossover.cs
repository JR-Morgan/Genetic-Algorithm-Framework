using CSP.Search.Initialisation;
using SearchExtensions;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSP.Search.Crossover
{
    class OrderedActivityCrossover : GenerationOperation<ISolution>
    {
        private static readonly Random random = new Random(); //TODO

        private readonly InitalisationStrategy repairStrategy;


        public OrderedActivityCrossover(ISelectionStrategy<ISolution> selectionStrategy, InitalisationStrategy repairStrategy, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IGenerationOperation<ISolution>? next = default)
            :base(selectionStrategy, elitismProportion, next)
        {
            this.repairStrategy = repairStrategy;
        }


        protected override ISolution[] OperateOnSelection(ISolution[] selected, ICostFunction<ISolution> fitnessFunction, int elite)
        {
            ISolution[] children = new ISolution[selected.Length];

            ISolution[] pool = (ISolution[])selected.Clone();
            pool.Shuffle(random);

            
            for (int i = 0; i < elite; i++)
            {
                children[i] = selected[i];
            }

            for (int i = elite; i < selected.Length; i++)
            {
                children[i] = Crossover(selected[i], pool[selected.Length - i - 1]);

                repairStrategy.Repair(children[i]);
            }

            return children;

        }


        private ISolution Crossover(ISolution parent1, ISolution parent2)
        {
            parent1 = parent1.Copy();
            parent2 = parent2.Copy();

            int geneA = random.Next(parent1.Activities.Count);
            int geneB = random.Next(parent1.Activities.Count);//does this assume both parents have the same number of activities?

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

            IEnumerable<Activity> ChildP2 = parent2.Activities.Except(ChildP1);

            ChildP1.AddRange(ChildP2);


            return new Solution(parent1.Problem, ChildP1);
        }

    }
}
