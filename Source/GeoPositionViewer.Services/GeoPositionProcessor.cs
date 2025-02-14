using GeoPositionViewer.Models;

namespace GeoPositionViewer.Services
{
    internal class GeoPositionProcessor : IGeoPositionProcessor
    {
        public GeoAveragePosition GetAveragePosition(IEnumerable<Position> positions)
        {
            if (positions == null || !positions.Any())
            {
                return GeoAveragePosition.Empty;
            }
            var latitude = positions.Average(p => p.Latitude);
            var longitude = positions.Average(p => p.Longitude);
            return new GeoAveragePosition(new Position(latitude, longitude), positions.Count());
        }
    }
}