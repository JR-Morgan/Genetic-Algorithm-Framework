using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TSP;
using TSP_WPF.ViewModels;

namespace TSP_WPF.Views
{
    /// <summary>
    /// Interaction logic for NewGraphDialogue.xaml
    /// </summary>
    public partial class NewGraphDialogue : Window
    {
        public GraphViewModel graphViewModel { get; private set; }
        public NewGraphDialogue(GraphViewModel graphViewModel)
        {
            InitializeComponent();
            this.graphViewModel = graphViewModel;
            this.DataContext = graphViewModel;
        }

        private void TextBoxIntOnly(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }


        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            /*
            Return = new GraphViewModel()
            {
                BoundX = Int32.Parse(txtBoundX.Text),
                BoundY = Int32.Parse(txtBoundY.Text),
                NodeCount = Int32.Parse(txtNodeCount.Text),
                Seed = Int32.Parse(txtSeed.Text),
            };*/
            this.DialogResult = true;
        }

    }
}
