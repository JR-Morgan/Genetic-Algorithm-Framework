namespace TSP.Solution_Stratergies
{
    public interface ISearchStratergy
    {
        public delegate void ItterationCompleteEventHandler(ISearchStratergy sender, Log log);

        public event ItterationCompleteEventHandler? OnItterationComplete;
        public void Compute(Graph graph);
    }
}
