using SearchStrategies.Operations;

namespace SearchStrategies.GenerationStrategies
{
    public interface IGenerationStrategy<S>
    {
        S[] NextGeneration(S[] population, IFitnessFunction<S> fitnessFunction);
    }
}
