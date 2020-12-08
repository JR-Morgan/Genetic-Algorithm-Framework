using SearchStrategies.Operations;

namespace SearchStrategies.GenerationStrategies
{
    public interface IGenerationOperation<S>
    {
        S[] Operate(S[] population, ICostFunction<S> fitnessFunction);
    }
}
