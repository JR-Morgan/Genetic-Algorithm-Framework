namespace TSP.Solution_Stratergies
{
    /// <summary>
    /// A search strategy for computing the best <see cref="Route"/> in a given <see cref="Graph"/>
    /// </summary>
    public interface ISearchStrategy
    {
        public delegate void ItterationCompleteEventHandler(ISearchStrategy sender, Log log);

        public event ItterationCompleteEventHandler? OnItterationComplete;
        public Log Compute(Graph graph);
    }
}
