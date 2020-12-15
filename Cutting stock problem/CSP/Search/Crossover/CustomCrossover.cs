using CSP.Search.Initialisation;
using SearchExtensions;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Crossover
{
    class CustomCrossover : GenerationOperation<ISolution>
    {
        private readonly Random random;

        private readonly InitalisationStrategy repairStrategy;

        internal CustomCrossover(ISelectionStrategy<ISolution> selectionStrategy, InitalisationStrategy repairStrategy, Random random, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, params IGenerationOperation<ISolution>[] next)
            : this(selectionStrategy, repairStrategy, random, elitismProportion, (IList<IGenerationOperation<ISolution>>)next)
        { }

        public CustomCrossover(ISelectionStrategy<ISolution> selectionStrategy, InitalisationStrategy repairStrategy, Random random, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IList<IGenerationOperation<ISolution>>? next = default)
            : base(selectionStrategy, elitismProportion, next)
        {
            this.random = random;
            this.repairStrategy = repairStrategy;
        }

        //TODO This crossover should produce decent solutions, but is so much worse than 3 point crossover.
        protected override IList<ISolution> OperateOnSelection(IList<ISolution> selection, ICostFunction<ISolution> fitnessFunction, int elite)
        {
            ISolution[] children = new ISolution[selection.Count];

            List<ISolution> pool = new(selection);


            pool.Shuffle(random);



            for (int i = elite; i < selection.Count; i++)
            {
                children[i] = Crossover(selection[i], pool[selection.Count - i - 1]);

                repairStrategy.Repair(children[i]);
            }

            return children;

        }

        private ISolution Crossover(params ISolution[] parents) => Crossover((IList<ISolution>)parents);
        private ISolution Crossover(IList<ISolution> parents)
        {
            int childSize = -1;
            foreach (ISolution parent in parents)
            {
                childSize = Math.Max(childSize, parent.Activities.Count);
            }

            List<Activity> activities = new();

            for (int i = 0; i < childSize; i++)
            {
                Activity? bestActivity = default;
                float bestActivityCost = float.PositiveInfinity;
                foreach (ISolution parent in parents)
                {
                    if(parent.Activities.Count > i)
                    {
                        float cost = parent.Activities[i].Cost;
                        if(cost < bestActivityCost)
                        {
                            bestActivity = parent.Activities[i];
                        }
                    }
                }
                activities.Add(bestActivity.Copy());

            }
            activities.Shuffle(random);
            Solution solution = new Solution(parents[0].Problem, activities);
            repairStrategy.Repair(solution);
            return solution;
        }

    }
}
