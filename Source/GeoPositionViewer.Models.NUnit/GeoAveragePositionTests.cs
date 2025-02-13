using GeoPositionViewer.Models;
using NUnit.Framework;

namespace GeoPositionViewer.Tests
{
    [TestFixture]
    public class GeoAveragePositionTests
    {
        [Test]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var position = new Position(10.0, 20.0);
            int count = 5;

            // Act
            var geoAveragePosition = new GeoAveragePosition(position, count);

            // Assert
            Assert.That(geoAveragePosition.AveragePosition, Is.EqualTo(position));
            Assert.That(geoAveragePosition.PositionsCount, Is.EqualTo(count));
        }

        [Test]
        public void Empty_ReturnsExpectedValues()
        {
            // Arrange & Act
            var empty = GeoAveragePosition.Empty;

            // Assert
            Assert.That(empty.AveragePosition, Is.EqualTo(Position.Empty));
            Assert.That(empty.PositionsCount, Is.EqualTo(0));
        }
    }
}
