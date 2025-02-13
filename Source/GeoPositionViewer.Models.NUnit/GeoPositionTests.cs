using GeoPositionViewer.Models;

namespace GeoPositionViewer.Tests
{
    [TestFixture]
    public class GeoPositionTests
    {
        [Test]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var position = new Position(10.0, 20.0);
            var timestamp = DateTime.UtcNow;

            // Act
            var geoPosition = new GeoPosition(position, timestamp);

            // Assert
            Assert.That(geoPosition.Position, Is.EqualTo(position));
            Assert.That(geoPosition.Timestamp, Is.EqualTo(timestamp));
        }

        [Test]
        public void Empty_ReturnsExpectedValues()
        {
            // Arrange & Act
            var empty = GeoPosition.Empty;

            // Assert
            Assert.That(empty.Position, Is.EqualTo(Position.Empty));
            Assert.That(empty.Timestamp, Is.EqualTo(DateTime.MinValue));
        }
    }
}
