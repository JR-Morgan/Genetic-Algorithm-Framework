using CSP.Search;
using SearchStrategies;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.ParameterOptimiser
{
    class AlgorithmCost : ICostFunction<EAParams>
    {
        private float timeOut;
        private Problem problem;

        public AlgorithmCost(float timeOut, Problem problem)
        {
            this.timeOut = timeOut;
            this.problem = problem;
        }

        int counter = 255;
        public float Cost(EAParams solution)
        {
            var ea = SearchFactory.EAA(TerminalStrategies.TimeOut(timeOut), timeOut / 5, solution, counter++);
            Log log = ea.Compute(problem);
            return log.bestSolutionFitness;
        }

        public EAParams Fittest(IEnumerable<EAParams> solutions)
        {
            EAParams? bestSolution = null;
            float bestSolutionCost = float.PositiveInfinity;

            foreach (EAParams s in solutions)
            {
                float cost = Cost(s);
                if (cost < bestSolutionCost)
                {
                    bestSolution = s;
                    bestSolutionCost = cost;
                }
            }

            return bestSolution ?? throw new Exception("Could not find best solution");
        }

        public EAParams LowestCost(IEnumerable<EAParams> solutions) => Fittest(solutions);
    }
}