namespace GeoPositionViewer.Models
{
    public class GeoPosition
    {
        public Position Position { get; }
        public DateTime Timestamp { get; }


        public GeoPosition(Position position, DateTime timestamp)
        {
            Position = position;
            Timestamp = timestamp;
        }

        public static GeoPosition Empty = new GeoPosition(Position.Empty, DateTime.MinValue);
    }
}
