using System;
using System.Collections.Generic;

namespace SearchStrategies
{
    public struct Log
    {
        public static readonly string CSV_TITLE = "EvalS, Time, Cost, Best";
        public int numberOfSolutionsEvaluated;
        public int iteration;
        public float bestSolutionFitness;
        public float timeToCompute;
        public string bestSolution;

        public override string ToString()
        {
            return $"Solutions evaluated: {numberOfSolutionsEvaluated}\n" +
                $"Time to compute: {timeToCompute}ms\n" +
                $"Best solution cost: {bestSolutionFitness}\n" +
                $"Best solution : {bestSolution}\n";

        }

        public string ToCSV() => $"{numberOfSolutionsEvaluated},{timeToCompute},{bestSolutionFitness},{bestSolution}";
    }
}
