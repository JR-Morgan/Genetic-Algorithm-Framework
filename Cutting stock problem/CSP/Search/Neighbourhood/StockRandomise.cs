using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;
using System;
using System.Collections.Generic;

namespace CSP.Search.Neighbourhood
{
    class StockRandomise : IGenerationOperation<ISolution>, INeighbourhood<ISolution>
    {
        private const int TIMEOUT = 50;
        private static readonly Random random = new Random(); //TODO determinism

        private readonly bool allowInvalid;
        public StockRandomise(bool allowInvalid = false)
        {
            this.allowInvalid = allowInvalid;
        }


        private ISolution Swap(ISolution parent, int i)
        {
            parent = parent.Copy();
            Stock RandomStock() => parent.Problem.Stock[random.Next(parent.Problem.Stock.Length)];
            Activity activity = parent.Activities[i];
            int counter = 0;
            do
            {
                activity.Stock = RandomStock();
            } while ((!allowInvalid) && activity.IsValid && ++counter > TIMEOUT);

            return parent;

        }

        public ISolution Swap(ISolution parent) => Swap(parent, random.Next(parent.Activities.Count));

        public List<ISolution> GenerateNeighbourhood(ISolution parent)
        {
            List<ISolution> solutions = new();
            for(int i = 0; i< parent.Activities.Count; i++)
            {
                solutions.Add(Swap(parent, i));
            }
            return solutions;
        }

        public ISolution[] Operate(ISolution[] population, IFitnessFunction<ISolution> fitnessFunction)
        {
            throw new NotImplementedException();
        }
    }
}
