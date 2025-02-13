using GeoPositionViewer.App.Views;
using System.Windows;

namespace GeoPositionViewer.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(PositionView positionView)
        {
            InitializeComponent();
            ContentArea.Content = positionView;
        }
    }
}