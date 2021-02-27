using System.Collections.Generic;

namespace SearchStrategies.Operations
{
    public interface ISearchStrategy<S,P>
    {
        public delegate void ItterationCompleteEventHandler(ISearchStrategy<S, P> sender, Log log);

        public event ItterationCompleteEventHandler? OnItterationComplete;
        
        public S? BestSolution { get; }
        public Log Compute(P problem);
    }
}
