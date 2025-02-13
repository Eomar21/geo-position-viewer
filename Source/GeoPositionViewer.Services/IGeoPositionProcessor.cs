using GeoPositionViewer.Models;

namespace GeoPositionViewer.Services
{
    public interface IGeoPositionProcessor
    {
        GeoAveragePosition GetAveragePosition(IEnumerable<Position> positions);
    }
}