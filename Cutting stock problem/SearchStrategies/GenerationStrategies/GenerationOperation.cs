using SearchStrategies.GenerationStrategies.Selection;
using SearchStrategies.Operations;

namespace SearchStrategies.GenerationStrategies
{
    public abstract class GenerationOperation<S> : IGenerationOperation<S>
    {
        protected const float DEFAULT_ELITEISM_PROPORTION = 0f;

        private IGenerationOperation<S>? next;
        private ISelectionStrategy<S> selectionStrategy;
        private float elitismProportion;

        public GenerationOperation(ISelectionStrategy<S> selectionStrategy, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IGenerationOperation<S>? next = default)
        {
            this.selectionStrategy = selectionStrategy;
            this.next = next;
            this.elitismProportion = elitismProportion;
        }

        public GenerationOperation(float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IGenerationOperation<S>? next = default) : this(new SelectAll<S>(), elitismProportion, next) { }


        public S[] Operate(S[] population, ICostFunction<S> fitnessFunction)
        {
            S[] offspring;
            offspring = OperateOnSelection(selectionStrategy.Select(population, fitnessFunction), fitnessFunction, (int)elitismProportion * population.Length);

            return next != null? next.Operate(offspring, fitnessFunction) : offspring;
        }
        protected abstract S[] OperateOnSelection(S[] selected, ICostFunction<S> fitnessFunction, int elite);
    }
}
