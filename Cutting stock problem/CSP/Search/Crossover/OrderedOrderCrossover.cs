using SearchStrategies.GenerationStrategies;
using SearchStrategies.Operations;

namespace CSP.Search.Crossover
{
    class OrderedOrderCrossover : GenerationOperation<ISolution>
    {
        public OrderedOrderCrossover(ISelectionStrategy<ISolution> selectionStrategy, float eliteismProportion = DEFAULT_ELITEISM_PROPORTION, IGenerationOperation<ISolution>? next = default)
        : base(selectionStrategy, eliteismProportion, next) { }

        protected override ISolution[] OperateOnSelection(ISolution[] population, ICostFunction<ISolution> fitnessFunction, int elite)
        {
            throw new System.NotImplementedException();
        }
    }
}
