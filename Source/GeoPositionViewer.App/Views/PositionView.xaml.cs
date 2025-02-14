using GeoPositionViewer.App.ViewModels;
using System.Windows.Controls;

namespace GeoPositionViewer.App.Views
{
    /// <summary>
    /// Interaction logic for PositionView.xaml
    /// </summary>
    public partial class PositionView : UserControl
    {
        public PositionView(PositionViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}