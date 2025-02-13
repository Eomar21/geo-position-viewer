using GeoPositionViewer.Models;
using Microsoft.Extensions.Hosting;

namespace GeoPositionViewer.Services
{
    public class PositionSimulator : BackgroundService
    {
        public event EventHandler<GeoPosition>? PositionGenerated;
        private const int m_OutputRate = 100;
        private const double m_Nootdorp_Lat = 52.042760;
        private const double m_Nootdorp_Long = 4.380580;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var newRandomGeoPosition = GeoRandomGeoPosition();
                PositionGenerated?.Invoke(this, newRandomGeoPosition);
                await Task.Delay(m_OutputRate, stoppingToken);
            }
        }

        private GeoPosition GeoRandomGeoPosition()
        {
            var currentPosition = new GeoPosition(new Position(m_Nootdorp_Lat, m_Nootdorp_Long), DateTime.UtcNow);
            // degree to meter conversion
            const double metersPerDegree = 111_000.0;

            // offset in degrees for roughly 5 meter
            const double maxDegreeOffset = 5.0 / metersPerDegree;

            Random random = new Random();

            double latitudeOffset = (random.NextDouble() * maxDegreeOffset * 2) - maxDegreeOffset;
            double longitudeOffset = (random.NextDouble() * maxDegreeOffset * 2) - maxDegreeOffset;

            double newLatitude = currentPosition.Position.Latitude + latitudeOffset;
            double newLongitude = currentPosition.Position.Longitude + longitudeOffset;

            return new GeoPosition(new Position(newLatitude, newLongitude), DateTime.UtcNow);
        }

        // Added this method for unit testing - not the best approach - and no code shall be added only for unit testing
        // but did this for now as this is simulator class and would be replaced either way in production.
        public void TriggerPositionGenerated(GeoPosition position)
        {
            PositionGenerated?.Invoke(this, position);
        }
    }
}
