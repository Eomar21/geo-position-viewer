using GeoPositionViewer.Models;
using GeoPositionViewer.Services;

namespace GeoPositionViewer.Services.NUnits
{
    [TestFixture]
    public class GeoPositionProcessorTests
    {
        private IGeoPositionProcessor m_GeoPositionProcessor;

        [SetUp]
        public void SetUp()
        {
            m_GeoPositionProcessor = new GeoPositionProcessor();
        }

        [Test]
        public void GetAveragePosition_ShouldReturnEmpty_ForNullInput()
        {
            // Arrange
            IEnumerable<Position> positions = null;

            // Act
            var result = m_GeoPositionProcessor.GetAveragePosition(positions);

            // Assert
            Assert.That(result, Is.EqualTo(GeoAveragePosition.Empty));
        }

        [Test]
        public void GetAveragePosition_ShouldReturnEmpty_ForEmptyList()
        {
            // Arrange
            var positions = new List<Position>();

            // Act
            var result = m_GeoPositionProcessor.GetAveragePosition(positions);

            // Assert
            Assert.That(result, Is.EqualTo(GeoAveragePosition.Empty));
        }

        [Test]
        public void GetAveragePosition_ShouldCalculateAverage_ForSinglePosition()
        {
            // Arrange
            var positions = new List<Position>
            {
                new Position(50.0, 10.0)
            };

            // Act
            var result = m_GeoPositionProcessor.GetAveragePosition(positions);

            // Assert
            Assert.That(result.AveragePosition.Latitude, Is.EqualTo(50.0));
            Assert.That(result.AveragePosition.Longitude, Is.EqualTo(10.0));
            Assert.That(result.PositionsCount, Is.EqualTo(1));
        }

        [Test]
        public void GetAveragePosition_ShouldCalculateAverage_ForMultiplePositions()
        {
            // Arrange
            var positions = new List<Position>
            {
                new Position(50.0, 10.0),
                new Position(60.0, 20.0),
                new Position(70.0, 30.0)
            };

            // Act
            var result = m_GeoPositionProcessor.GetAveragePosition(positions);

            // Assert
            Assert.That(result.AveragePosition.Latitude, Is.EqualTo(60.0));
            Assert.That(result.AveragePosition.Longitude, Is.EqualTo(20.0));
            Assert.That(result.PositionsCount, Is.EqualTo(3));
        }
    }
}
