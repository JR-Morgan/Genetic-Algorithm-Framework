using System;

namespace SearchStrategies.GenerationStrategies.Selection
{
    public class ProabalisticSelection<S> : FunctionSelection<S>
    {

        public ProabalisticSelection(float proababilty, Random random) : base(Function(proababilty, random)) { }

        private static Func<S,bool> Function(float proababilty, Random random)
        {
            bool f(S? s = default) => random.NextDouble() < proababilty;
            return f;
        }
    }
}
