using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Mutation
{
    class OrderCrossover : GenerationOperation<ISolution>
    {
        private const int DEFAULT_MUTATION_RATE = 1;
        private readonly int mutationScale;
        private readonly Random random;

        private bool allowInvalid;
        internal OrderCrossover(ISelectionStrategy<ISolution> selectionStrategy, Random random, int mutationScale = 10, bool allowInvalid = true, float eliteismProportion = DEFAULT_ELITEISM_PROPORTION, params IGenerationOperation<ISolution>[] next)
        : this(selectionStrategy, random, mutationScale, allowInvalid, eliteismProportion, (IList<IGenerationOperation<ISolution>>)next)
        { }

        public OrderCrossover(ISelectionStrategy<ISolution> selectionStrategy, Random random, int mutationScale = DEFAULT_MUTATION_RATE, bool allowInvalid = true, float eliteismProportion = DEFAULT_ELITEISM_PROPORTION, IList<IGenerationOperation<ISolution>>? next = default)
        : base(selectionStrategy, eliteismProportion, next)
        {
            this.random = random;
            this.mutationScale = mutationScale;
            this.allowInvalid = allowInvalid;
        }

        protected override IList<ISolution> OperateOnSelection(IList<ISolution> selection, ICostFunction<ISolution> fitnessFunction, int elite)
        {
            List<ISolution> offspring = new(selection.Count);
            foreach (ISolution parent in selection)
            {
                ISolution child = parent.Copy();
                offspring.Add(child);

                //mutate the child
                int counter = 0;

                while (counter++ < mutationScale)
                {
                    Activity activityA = child.Activities[random.Next(child.Activities.Count)],
                             activityB = child.Activities[random.Next(child.Activities.Count)];

                    int geneA = random.Next(activityA.Orders.Count),
                        geneB = random.Next(activityB.Orders.Count);

                    float lengthA = activityA.Orders[geneA],
                          lengthB = activityB.Orders[geneB];



                    if (allowInvalid
                     || activityA.RemainingLength - lengthA + lengthB >= 0
                     && activityB.RemainingLength - lengthB + lengthA >= 0)
                    {
                        activityA.Remove(lengthA);
                        activityA.AddUnchecked(lengthB);
                        activityB.Remove(lengthB);
                        activityB.AddUnchecked(lengthA);

                        //break;
                    }

                } 


            }
            return offspring;

        }
    }
}
