using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using TSP;
using TSP.Solution_Stratergies;
using TSP.Solution_Stratergies.LocalSearch;
using TSP_WPF.ViewModels;

namespace TSP_WPF.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly RunViewModel runViewModel;
        private readonly GraphViewModel graphViewModel;

        public Window1()
        {
            InitializeComponent();
            runViewModel = new RunViewModel();
            DataContext = runViewModel;

            List<ISearchStrategy> strategies = MyLocalSearches.GenerateSearchesTimeOut();

            cboOptions.ItemsSource = strategies;
            cboOptions.SelectedIndex = strategies.Count - 1;
            graphViewModel = new GraphViewModel()
            {
                BoundX = 100,
                BoundY = 100,
                NodeCount = 25,
                Seed = 255
            };
            runViewModel.Graph = graphViewModel.ToGraph();

            ReadyToComputeCheck();
        }


        private void Compute()
        {
            if (runViewModel.SearchStrategy != null && runViewModel.Graph != null)
            {
                runViewModel.NewSeries();
                Task.Run(() => runViewModel.SearchStrategy.Compute(runViewModel.Graph));
            }
                
        }



        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            Compute();
        }

        private void btnNewGraph_Click(object sender, RoutedEventArgs e)
        {
            var graphDialog = new NewGraphDialogue(graphViewModel);

            bool? r = graphDialog.ShowDialog();
            if (r != null && (bool)r)
            {
                runViewModel.Graph = graphDialog.graphViewModel.ToGraph();
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
                //TODO console 
                runViewModel.Add(log.timeToCompute, log.bestRouteCost); ;
                pltPlot.InvalidatePlot(true);
            }
        }

        private void btnLoadGraphFromFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "comma separated values (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    runViewModel.Graph = Graph.ParseGraphFromFile(openFileDialog.FileName);
                }
            }
        }
        private void cboOptions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ChangeSearchStrategy((ISearchStrategy)cboOptions.SelectedItem);
        }
    }
}
