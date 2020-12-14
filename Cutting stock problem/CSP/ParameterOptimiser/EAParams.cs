using System;

namespace CSP.ParameterOptimiser
{
    public struct EAParams
    {
        internal int n { get; set; }
        internal uint populationSize { get; set; }
        internal float eliteProportion { get; set; }
        internal float comparisionProportion { get; set; }
        internal float selectionProportion { get; set; }
        internal float mutationRate { get; set; }
        internal float mutationRate2 { get; set; }
        internal int mutationScale { get; set; }

        internal uint selectionSize => (uint)Math.Max(2, populationSize * selectionProportion);
        internal uint K => (uint)Math.Max(2, populationSize * comparisionProportion);



        public override string ToString()
        {
            return $"{populationSize},{n},{eliteProportion},{comparisionProportion},{selectionProportion},{mutationRate},{mutationRate2},{mutationScale}";
        }
    }
}