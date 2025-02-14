using GeoPositionViewer.App.ViewModels;
using System.Windows;

namespace GeoPositionViewer.App
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
}