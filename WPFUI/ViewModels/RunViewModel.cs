using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
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
        private LineSeries? series;

        public RunViewModel()
        {

            var tmp = new PlotModel {
                Title = Title,
                Subtitle = "Travelling Salesman Problem",
                IsLegendVisible = true,           
                };
            tmp.Legends.Add(new Legend());

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time to Compute (ms)",
                Minimum = 0d,
                ExtraGridlines = new double[] { 0 },
            };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Route Cost (a.u.)",
                Minimum = 0d,
                ExtraGridlines = new double[] { 0 },
            };

            tmp.Axes.Add(xAxis);
            tmp.Axes.Add(yAxis);

            this.Model = tmp;
        }

        public void NewSeries()
        {
            if(SearchStrategy != null)
            {
                series = new LineSeries { Title = SearchStrategy.ToString(), MarkerType = MarkerType.Circle};
                Model.Series.Add(series);
            }
        }

        public ISearchStrategy? SearchStrategy { get; set; }
        public Graph? Graph { get; set; }

        public bool IsReady => SearchStrategy != null && Graph != null;


        public void Add(float time, float cost)
        {
            series?.Points.Add(new DataPoint(time, cost));
        }
    }
}
