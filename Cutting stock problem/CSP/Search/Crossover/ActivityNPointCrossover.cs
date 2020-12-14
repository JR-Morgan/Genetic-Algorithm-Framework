using CSP.Search.Initialisation;
using SearchExtensions;
using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace CSP.Search.Crossover
{
    class ActivityNPointCrossover : GenerationOperation<ISolution>
    {
        private readonly Random random = new Random(); //TODO

        private readonly InitalisationStrategy repairStrategy;
        private readonly int n;

        internal ActivityNPointCrossover(int n, ISelectionStrategy<ISolution> selectionStrategy, InitalisationStrategy repairStrategy, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, params IGenerationOperation<ISolution>[] next)
            : this(n, selectionStrategy, repairStrategy, elitismProportion, (IList<IGenerationOperation<ISolution>>) next)
        { }

        public ActivityNPointCrossover(int n, ISelectionStrategy<ISolution> selectionStrategy, InitalisationStrategy repairStrategy, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IList<IGenerationOperation<ISolution>>? next = default)
            :base(selectionStrategy, elitismProportion, next)
        {
            this.repairStrategy = repairStrategy;
            this.n = n;
        }


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
            int minSize = Int32.MaxValue;
            foreach (ISolution parent in parents)
            {
                minSize = Math.Min(minSize, parent.Activities.Count);
            }

            List<int> points = new(n);
            for(int i = 0; i < n; i++)
            {
                points.Add(random.Next(minSize));
            }

            points.Sort();

         
            return Crossover(parents, points);
        }

        protected ISolution Crossover(IList<ISolution> parents, IList<int> points)
        {
            int childSize = -1;
            foreach (ISolution parent in parents)
            {
                childSize = Math.Max(childSize, parent.Activities.Count);
            }

            List<Activity> child = new(childSize);

            int pi = 0;
            int i = 0;
            foreach(int p in points)
            {
                for(;i<p; i++)
                {
                    child.Add(parents[pi].Activities[i].Copy());
                }
                pi = ((pi + 1) % parents.Count);
            }

            return new Solution(parents[0].Problem, child) ;
        }

    }
}
