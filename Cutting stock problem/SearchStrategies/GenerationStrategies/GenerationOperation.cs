using SearchStrategies.GenerationStrategies.Replacement;
using SearchStrategies.GenerationStrategies.Selection;
using SearchStrategies.Operations;
using System.Collections.Generic;
using System.Linq;

namespace SearchStrategies.GenerationStrategies
{
    /// <summary>
    /// Encapsulates a type of <see cref="IGenerationOperation{S}"/> that includes selection in the form of <see cref="ISelectionStrategy{S}"/>
    /// and a singly linked next <see cref="IGenerationOperation{S}"/> 
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public abstract class GenerationOperation<S> : IGenerationOperation<S>
    {
        protected const float DEFAULT_ELITEISM_PROPORTION = 0f;
        private static readonly IReplacementStrategy<S> replaceParents = new ReplaceParents<S>();

        private IList<IGenerationOperation<S>> next;
        private ISelectionStrategy<S> selectionStrategy;
        private float elitismProportion;

        public GenerationOperation(ISelectionStrategy<S> selectionStrategy, float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IList<IGenerationOperation<S>>? next = default)
        {
            this.selectionStrategy = selectionStrategy;
            this.next = next?? new IGenerationOperation<S>[0];
            this.elitismProportion = elitismProportion;
        }

        public GenerationOperation(float elitismProportion = DEFAULT_ELITEISM_PROPORTION, IList<IGenerationOperation<S>>? next = default) : this(new SelectAll<S>(), elitismProportion, next) { }


        public IList<(S solution, int index)> Operate(IList<S> population, ICostFunction<S> fitnessFunction)
        {
            IList<(S, int)> parents = selectionStrategy.Select(population, fitnessFunction);

            S[] selected = parents.Select(item => item.Item1).ToArray();

            IList<S> children = OperateOnSelection(selected, fitnessFunction, (int)elitismProportion * population.Count);

            

            foreach(IGenerationOperation<S> n in next)
            {
                replaceParents.Replace(ref children, n.Operate(children, fitnessFunction));
            }

            return children.Select((item, index) => (item, parents[index].Item2)).ToArray();
        }

        /// <summary>
        /// Performs a generation operation on the selected solutions.
        /// </summary>
        /// <param name="selection">An array of <typeparamref name="S"/></param>
        /// <param name="fitnessFunction">The fitness function to use</param>
        /// <param name="elite">The number of elite</param>
        /// <returns>An array of equal size to selection, of new solutions that form the offspring</returns>
        protected abstract IList<S> OperateOnSelection(IList<S> selection, ICostFunction<S> fitnessFunction, int elite);


        //TODO Operation returns subset of population and this is being returned as the new generation,
        //This isn't working correctly for the nested operations because only the subset that the last operation returns is added as the new population.
    }
}
