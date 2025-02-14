namespace GeoPositionViewer.Models.Extensions
{
    public static class PositionsExtensions
    {
        public static Position RoundValue(this Position position, int digits)
        {
            return new Position(Math.Round(position.Latitude, digits), Math.Round(position.Longitude, digits));
        }

        public static GeoPosition RoundValue(this GeoPosition geoPosition, int digits)
        {
            return new GeoPosition(
                new Position(Math.Round(geoPosition.Position.Latitude, digits), Math.Round(geoPosition.Position.Longitude, digits)),
                geoPosition.Timestamp);
        }

        public static GeoAveragePosition RoundValue(this GeoAveragePosition geoAveragePosition, int digits)
        {
            return new GeoAveragePosition(
                new Position(Math.Round(geoAveragePosition.AveragePosition.Latitude, digits), Math.Round(geoAveragePosition.AveragePosition.Longitude, digits)),
                geoAveragePosition.PositionsCount);
        }
    }
}