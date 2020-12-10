namespace CSP.ParameterOptimiser
{
    public struct Parameters
    {
        internal uint populationSize { get; set; }
        internal float eliteProportion { get; set; }
        internal float comparisionProportion { get; set; }
        internal float selectionProportion { get; set; }
        internal float mutationRate { get; set; }


        public override string ToString()
        {
            return $"P:{populationSize} E:{eliteProportion} K: {comparisionProportion} S: {selectionProportion} M: {mutationRate}";
        }
    }
}