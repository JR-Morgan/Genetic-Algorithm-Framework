using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using TSP;
using TSP.Solution_Stratergies;

namespace WPFUI.ViewModels
{
    public class RunViewModel
    {
        private ISearchStrategy? searchStrategy;

        public string Title => searchStrategy != null? searchStrategy.ToString() : "";
        public PlotModel Model { get; private set; }
        public LineSeries series;

        public RunViewModel()
        {
            var tmp = new PlotModel { Title = Title, Subtitle = "Travling Salesman Problem" };

            series = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };

            tmp.Series.Add(series);

            this.Model = tmp;
        }



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


        public void Add(float time, float cost)
        {
            series.Points.Add(new DataPoint(time, cost));
        }
    }
}
