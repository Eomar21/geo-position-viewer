namespace GeoPositionViewer.Models
{
    public class GeoAveragePosition
    {
        public Position AveragePosition { get; set; }
        public int PositionsCount { get; set; }

        public GeoAveragePosition(Position averagePosition, int positionsCount)
        {
            AveragePosition = averagePosition;
            PositionsCount = positionsCount;
        }

        public static GeoAveragePosition Empty = new GeoAveragePosition(Position.Empty, 0);
    }
}
