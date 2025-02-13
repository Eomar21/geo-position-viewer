using GeoPositionViewer.Models;

namespace GeoPositionViewer.Services
{
    public interface IPositionSimulator
    {
        event EventHandler<GeoPosition> PositionGenerated;
    }
}