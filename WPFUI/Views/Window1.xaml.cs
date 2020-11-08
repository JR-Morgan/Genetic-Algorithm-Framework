using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using TSP;
using TSP.Solution_Stratergies;
using TSP.Solution_Stratergies.LocalSearch;
using WPFUI.ViewModels;

namespace WPFUI.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private RunViewModel runViewModel;
        private GraphViewModel graphViewModel;
        public Window1()
        {
            InitializeComponent();
            runViewModel = new RunViewModel();
            DataContext = runViewModel;

            List<ISearchStrategy> strategies = new List<ISearchStrategy>
            {
                new ExhaustiveSearch(),
                MyLocalSearches.LS1(),
                MyLocalSearches.LS2(),
                MyLocalSearches.GN1(100, 5),
            };

            cboOptions.ItemsSource = strategies;
            cboOptions.SelectedIndex = 1;
            graphViewModel = new GraphViewModel()
            {
                BoundX = 100,
                BoundY = 100,
                NodeCount = 15,
                Seed = 255
            };
            runViewModel.graph = graphViewModel.ToGraph();

            ReadyToComputeCheck();
        }



        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            if(runViewModel.SearchStrategy != null && runViewModel.graph != null)
                runViewModel.SearchStrategy.Compute(runViewModel.graph);
        }

        private void btnNewGraph_Click(object sender, RoutedEventArgs e)
        {
            var graphDialog = new NewGraphDialogue(graphViewModel);

            bool? r = graphDialog.ShowDialog();
            if (r != null && (bool)r)
            {
                runViewModel.graph = graphDialog.graphViewModel.ToGraph();
                ReadyToComputeCheck();
            }
        }

        private void ReadyToComputeCheck()
        {
            btnCompute.IsEnabled = runViewModel.IsReady;
        }

        private void ChangeSearchStrategy(ISearchStrategy searchStrategy)
        {
            runViewModel.SearchStrategy = searchStrategy;
            searchStrategy.OnItterationComplete += ItterationCompleteHandler;

            if (searchStrategy != null)
                searchStrategy.OnItterationComplete += ItterationCompleteHandler;
            ReadyToComputeCheck();

            void ItterationCompleteHandler(ISearchStrategy sender, Log log)
            {
                runViewModel.Add(log.timeToCompute, log.bestRouteCost);;
                pltPlot.InvalidatePlot(true);
            }
        }


        private void cboOptions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ChangeSearchStrategy((ISearchStrategy)cboOptions.SelectedItem);
        }
    }
}
