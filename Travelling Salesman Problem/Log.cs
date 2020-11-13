namespace TSP
{
    public struct Log
    {
        public int numberOfRoutesEvaluated;
        public int itteration;
        public float bestRouteCost;
        public float timeToCompute;
        public int[] bestRoute;

        public override string ToString()
        {
            return $"Routes evaluated: {numberOfRoutesEvaluated}\n" +
                $"Time to compute: {timeToCompute}ms\n" +
                $"Best route cost: {bestRouteCost}\n" + 
                $"Best Route : {bestRoute}";
                
        }

    }
}
