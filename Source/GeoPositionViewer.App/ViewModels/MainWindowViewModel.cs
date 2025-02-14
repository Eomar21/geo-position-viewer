using GeoPositionViewer.App.Views;

namespace GeoPositionViewer.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public PositionView PositionView { get; }

        public MainWindowViewModel(PositionView positionView)
        {
            PositionView = positionView;
        }
    }
}