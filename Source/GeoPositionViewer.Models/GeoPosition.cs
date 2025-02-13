namespace GeoPositionViewer.Models
{
    public class GeoPosition
    {
        public Position Position { get; set; }
        public DateTime Timestamp { get; set; }


        public GeoPosition(Position position, DateTime timestamp)
        {
            Position = position;
            Timestamp = timestamp;
        }

        public static GeoPosition Empty = new GeoPosition(Position.Empty, DateTime.MinValue);
    }
}
