using SearchStrategies.GenerationStrategies.Replacement;
using SearchStrategies.Operations;
using System.Collections.Generic;

namespace SearchStrategies.GenerationStrategies
{
    
    public class Generation<S>
    {
        public delegate void Evaluate(S child);
        private IGenerationOperation<S>[] operations;
        private IReplacementStrategy<S> replacementStrategy;


        public Generation(IReplacementStrategy<S> replacementStrategy, params IGenerationOperation<S>[] operations)
        {
            this.operations = operations;
            this.replacementStrategy = replacementStrategy;
        }

        public IList<S> NextGeneration(IList<S> population, ICostFunction<S> fitnessFunction, Evaluate evaluate)
        {
            foreach (IGenerationOperation<S> operation in operations)
            {
                IList<(S, int)> s = operation.Operate(population, fitnessFunction);
                
                foreach((S child, int index) in s)
                {
                    evaluate(child);
                }

                replacementStrategy.Replace(ref population, s);
            }

            return population;
        }


    }
}
