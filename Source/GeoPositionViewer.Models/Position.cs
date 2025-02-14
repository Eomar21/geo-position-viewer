namespace GeoPositionViewer.Models
{
    public class Position
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Position Empty => new Position(0.0, 0.0);
    }
}
