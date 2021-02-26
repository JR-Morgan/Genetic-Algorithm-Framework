using System.Numerics;
using TSP;

namespace WPFUI.ViewModels
{
    public class GraphViewModel
    {
        public int Seed { get; init; }
        public int NodeCount { get; init; }
        public int BoundY { get; init; }
        public int BoundX { get; init; }
        public Graph ToGraph()
        {
            return Graph.RandomGraph((uint)NodeCount, new Vector2(BoundX, BoundY), Seed);
        }
    }
}
