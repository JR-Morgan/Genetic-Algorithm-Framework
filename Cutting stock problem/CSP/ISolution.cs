using System;
using System.Collections.Generic;
using System.Text;

namespace CSP
{
    interface ISolution
    {
        Problem Problem { get; }
        List<Activity> Activities { get; }
        float Fitness();

        ISolution Copy();
    }
}
