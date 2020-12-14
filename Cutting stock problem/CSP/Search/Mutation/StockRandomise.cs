using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Mutation
{
    class StockRandomise : IGenerationOperation<ISolution>, INeighbourhood<ISolution>
    {
        private readonly Random random = new Random(); //TODO determinism

        private readonly float mutationRate;
        private readonly bool allowInvalid;
        public StockRandomise(bool allowInvalid = false, float mutationRate = 1f)
        {
            this.allowInvalid = allowInvalid;
            this.mutationRate = mutationRate;
        }

        public IList<(ISolution solution, int index)> Operate(IList<ISolution> population, ICostFunction<ISolution>? fitnessFunction = default)
        {
            List<(ISolution, int )> children = new();

            for (int i = 0; i < population.Count; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    children.Add((Swap(population[i]), i));
                }
            }
            return children;
            
        }

        public ISolution Swap(ISolution parent) => Swap(parent, random.Next(parent.Activities.Count));

        private ISolution Swap(ISolution parent, int i)
        {
            parent = parent.Copy();
            Stock RandomStock() => parent.Problem.Stock[random.Next(parent.Problem.Stock.Length)];
            Activity activity = parent.Activities[i];
            do
            {
                activity.Stock = RandomStock();
            } while (!(allowInvalid || activity.IsValid));



            return parent;
        }

        public List<ISolution> GenerateNeighbourhood(ISolution parent)
        {
            List<ISolution> solutions = new();
            for(int i = 0; i< parent.Activities.Count; i++)
            {
                solutions.Add(Swap(parent, i));
            }
            return solutions;
        }

        
    }
}
