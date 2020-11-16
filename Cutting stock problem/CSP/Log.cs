namespace CSP
{
    public struct Log
    {
        public int numberOfSolutionsEvaluated;
        public int iteration;
        public float bestSolutionCost;
        public float timeToCompute;
        public string bestSolution;

        public override string ToString()
        {
            return $"Solutions evaluated: {numberOfSolutionsEvaluated}\n" +
                $"Time to compute: {timeToCompute}ms\n" +
                $"Best solution cost: {bestSolutionCost}\n" +
                $"Best solution : {bestSolution}\n";

        }

    }
}
