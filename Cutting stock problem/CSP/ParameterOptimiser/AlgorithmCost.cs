using CSP.Search;
using SearchStrategies;
using SearchStrategies.Operations;
using SearchStrategies.TerminalConditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.ParameterOptimiser
{
    class AlgorithmCost : ICostFunction<Parameters>
    {
        private float timeOut;
        private Problem problem;

        public AlgorithmCost(float timeOut, Problem problem)
        {
            this.timeOut = timeOut;
            this.problem = problem;
        }

        public float Cost(Parameters solution)
        {
            var ea = SearchFactory.EA1(TerminalStrategies.TimeOut(timeOut), solution);
            Log log = ea.Compute(problem);
            return log.bestSolutionFitness;
        }

        public Parameters Fittest(IEnumerable<Parameters> solutions)
        {
            Parameters? bestSolution = null;
            float bestSolutionCost = float.PositiveInfinity;

            foreach (Parameters s in solutions)
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
    }
}