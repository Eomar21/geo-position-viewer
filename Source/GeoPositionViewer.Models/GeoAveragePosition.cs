namespace GeoPositionViewer.Models
{
    public class GeoAveragePosition
    {
        public Position AveragePosition { get; }
        public int PositionsCount { get; }

        public GeoAveragePosition(Position averagePosition, int positionsCount)
        {
            AveragePosition = averagePosition;
            PositionsCount = positionsCount;
        }

        public static GeoAveragePosition Empty = new GeoAveragePosition(Position.Empty, 0);
    }
}