namespace TSP
{
    public struct Log
    {
        public int numberOfRoutesEvaluated;
        public int itteration;
        public float bestRouteCost;
        public float timeToCompute;

        public override string ToString()
        {
            return $"{numberOfRoutesEvaluated} valid routes found\n" +
                $"Took {timeToCompute}ms to compute\n" +
                $"fastest route is {bestRouteCost}\n";
                
        }

    }
}
