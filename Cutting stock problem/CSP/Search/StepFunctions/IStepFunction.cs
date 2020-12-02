using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.Search.StepFunctions
{
    interface IStepFunction
    {
        ISolution Fitness(IEnumerable<ISolution> solutions);

        ISolution FitnessP(params ISolution[] solutions) => Fitness(solutions);
    }
}
