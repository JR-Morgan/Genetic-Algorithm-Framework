using System;
using System.Collections.Generic;
using System.Text;

namespace SearchStrategies.Operations
{
    public interface ICrossover<S>
    {
        S CrossOver(S parent1, S parent2);
    }
}
