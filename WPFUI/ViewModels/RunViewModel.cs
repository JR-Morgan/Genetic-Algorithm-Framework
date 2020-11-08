using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using TSP;
using TSP.Solution_Stratergies;

namespace WPFUI.ViewModels
{
    public class RunViewModel
    {
        public string Title => "";
        public PlotModel Model { get; private set; }
        private LineSeries series;

        public RunViewModel()
        {
            var tmp = new PlotModel { Title = Title, Subtitle = "Travling Salesman Problem" };

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time to Compute (ms)",
            };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Route Cost (a.u.)",
            };

            tmp.Axes.Add(xAxis);
            tmp.Axes.Add(yAxis);

            this.Model = tmp;
        }

        public void NewSeries()
        {
            if(SearchStrategy != null)
            {
                series = new LineSeries { Title = SearchStrategy.ToString(), MarkerType = MarkerType.Circle };
                Model.Series.Add(series);
            }
        }

        public ISearchStrategy? SearchStrategy { get; set; }
        public Graph? graph { get; set; }

        public bool IsReady => SearchStrategy != null && graph != null;


        public void Add(float time, float cost)
        {
            series.Points.Add(new DataPoint(time, cost));
        }
    }
}
