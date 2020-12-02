using System;
using System.Collections.Generic;
using System.Text;

namespace CSP
{
    interface ISolution
    {
        List<Activity> Activities { get; }
        float Fitness();
    }
}
