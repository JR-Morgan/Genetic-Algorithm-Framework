using System;
using System.Collections.Generic;
using System.Text;

namespace CSP.Search.Initialisation
{
    interface IInitialise
    {
        ISolution Initalise(Problem problem);
    }
}
