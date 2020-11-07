using OxyPlot;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TSP;
using TSP.Solution_Stratergies;

namespace WPFUI.ViewModels
{
    public class RunViewModel
    {
        private ISearchStrategy? searchStrategy;

        public string Title { get; set; }
        public IList<DataPoint> Points { get; private set; }


        public ISearchStrategy? SearchStrategy
        {
            get => searchStrategy;
            set
            {
                searchStrategy = value;
            }
        }
        public Graph? graph { get; set; }

        public bool IsReady => SearchStrategy != null && graph != null;
        public RunViewModel()
        {
            Title = "";
            Points = new List<DataPoint>();
        }


        public void Add(float time, float cost)
        {
            Points.Add(new DataPoint(time, cost));
        }
    }
}
