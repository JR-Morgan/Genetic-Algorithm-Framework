namespace TSP.Solution_Stratergies
{
    public interface ISearchStrategy
    {
        public delegate void ItterationCompleteEventHandler(ISearchStrategy sender, Log log);

        public event ItterationCompleteEventHandler? OnItterationComplete;
        public void Compute(Graph graph);
    }
}
