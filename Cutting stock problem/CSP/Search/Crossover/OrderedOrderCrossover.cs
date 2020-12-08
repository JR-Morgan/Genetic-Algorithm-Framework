using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Crossover
{
    class OrderedOrderCrossover : GenerationOperation<ISolution>
    {
        private const int TIMEOUT = 20;
        private static readonly Random random = new Random(); //TODO

        private bool allowInvalid;
        public OrderedOrderCrossover(ISelectionStrategy<ISolution> selectionStrategy, bool allowInvalid = false, float eliteismProportion = DEFAULT_ELITEISM_PROPORTION, IGenerationOperation<ISolution>? next = default)
        : base(selectionStrategy, eliteismProportion, next)
        {
            this.allowInvalid = allowInvalid;
        }

        protected override IList<ISolution> OperateOnSelection(IList<ISolution> selection, ICostFunction<ISolution> fitnessFunction, int elite)
        {
            foreach (ISolution solution in selection)
            {
                int counter = 0;

                while (counter++ < TIMEOUT)
                {
                    Activity activityA = solution.Activities[random.Next(solution.Activities.Count)],
                             activityB = solution.Activities[random.Next(solution.Activities.Count)];

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

                        break;
                    }

                } 


            }
            return selection;

        }
    }
}
