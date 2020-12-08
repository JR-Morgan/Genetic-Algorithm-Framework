using SearchStrategies.GenerationStrategies.Replacement;
using SearchStrategies.Operations;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies
{
    public class Generation<S>
    {
        private IGenerationOperation<S>[] operations;
        private IReplacementStrategy<S> replacementStrategy;


        public Generation(IReplacementStrategy<S> replacementStrategy, params IGenerationOperation<S>[] operations)
        {
            this.operations = operations;
            this.replacementStrategy = replacementStrategy;
        }

        public IList<S> NextGeneration(IList<S> population, ICostFunction<S> fitnessFunction)
        {
            foreach (IGenerationOperation<S> operation in operations)
            {
                replacementStrategy.Replace(ref population, operation.Operate(population, fitnessFunction));
            }

            return population;
        }


    }
}
