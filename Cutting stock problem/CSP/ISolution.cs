using System.Collections.Generic;

namespace CSP
{
    public interface ISolution
    {
        Problem Problem { get; }
        internal List<Activity> Activities { get; }
        float Fitness();

        ISolution Copy();
    }
}
