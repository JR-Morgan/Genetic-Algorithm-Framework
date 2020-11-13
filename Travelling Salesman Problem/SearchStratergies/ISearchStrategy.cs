namespace TSP.Solution_Stratergies
{
    public interface ISearchStrategy
    {
        public delegate void ItterationCompleteEventHandler(ISearchStrategy sender, Log log);

        public event ItterationCompleteEventHandler? OnItterationComplete;
        public Log Compute(Graph graph);
    }
}
