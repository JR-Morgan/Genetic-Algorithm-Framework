using System.Collections.Generic;

namespace CSP
{
    public interface ISolution
    {
        Problem Problem { get; }
        internal List<Activity> Activities { get; }
        float Cost();

        ISolution Copy();
        bool IsValid();
        bool IsComplete();
    }
}
