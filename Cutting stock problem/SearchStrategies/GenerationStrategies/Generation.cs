using SearchExtensions;
using SearchStrategies.Operations;
using System;

namespace SearchStrategies.GenerationStrategies
{
    public class Generation<S> : IGenerationStrategy<S>
    {
        private IGenerationOperation<S>[] operations;

        public Generation(params IGenerationOperation<S>[] operations)
        {
            this.operations = operations;
        }

        public S[] NextGeneration(S[] population, IFitnessFunction<S> fitnessFunction)
        {
            return ApplyOperations(operations, population, fitnessFunction);
        }

        private static S[] ApplyOperations(IGenerationOperation<S>[] operations, S[] population, IFitnessFunction<S> fitnessFunction, int i = 0)
        {
            if(i < operations.Length - 1)
            {
                return operations[i].Operate(ApplyOperations(operations, population, fitnessFunction, i + 1), fitnessFunction);
            }
            return operations[i].Operate(population, fitnessFunction);
        }
    }
}
