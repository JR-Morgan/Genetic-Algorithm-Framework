namespace TSP
{
    /// <summary>
    /// Encapsulates the results of an search iteration
    /// </summary>
    public struct Log
    {
        public int numberOfRoutesEvaluated;
        public int iteration;
        public float bestRouteCost;
        public float timeToCompute;
        public string bestRoute;

        public override string ToString()
        {
            return $"Routes evaluated: {numberOfRoutesEvaluated}\n" +
                $"Time to compute: {timeToCompute}ms\n" +
                $"Best route cost: {bestRouteCost}\n" + 
                $"Best Route : {bestRoute}\n";
                
        }

    }
}
