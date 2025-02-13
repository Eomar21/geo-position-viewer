using GeoPositionViewer.App.ViewModels;
using GeoPositionViewer.Models;
using GeoPositionViewer.Services;
using Moq;

namespace GeoPositionViewer.Tests
{
    [TestFixture]
    public class PositionViewModelTests
    {
        private Mock<IGeoPositionProcessor> m_GeoPositionProcessorMock;
        private Mock<PositionSimulator> m_SimulatorMock;
        private PositionViewModel m_ViewModel;

        [SetUp]
        public void SetUp()
        {
            m_GeoPositionProcessorMock = new Mock<IGeoPositionProcessor>();
            m_SimulatorMock = new Mock<PositionSimulator>();

            m_GeoPositionProcessorMock.Setup(p => p.GetAveragePosition(It.IsAny<IEnumerable<Position>>()))
                .Returns((IEnumerable<Position> positions) =>
                {
                    var avgLat = positions.Average(p => p.Latitude);
                    var avgLon = positions.Average(p => p.Longitude);
                    return new GeoAveragePosition(new Position(avgLat, avgLon), positions.Count());
                });
            m_ViewModel = new PositionViewModel(m_GeoPositionProcessorMock.Object, m_SimulatorMock.Object);
        }

        [Test]
        public void GeoRawPosition_ShouldUpdate_OnPositionGenerated()
        {
            // Arrange
            var testPosition = new GeoPosition(new Position(50.0, 10.0), DateTime.UtcNow);
            m_SimulatorMock.Raise(s => s.PositionGenerated += null, testPosition);

            // Act
            var result = m_ViewModel.GeoRawPosition;

            // Assert
            Assert.That(result.Position.Latitude, Is.EqualTo(50.0));
            Assert.That(result.Position.Longitude, Is.EqualTo(10.0));
        }

        [Test]
        public void GeoPosition_ShouldUpdate_AfterThrottledInterval()
        {
            // Arrange
            var testPosition = new GeoPosition(new Position(50.0, 10.0), DateTime.UtcNow);

            m_SimulatorMock.Raise(s => s.PositionGenerated += null, testPosition);
            m_SimulatorMock.Raise(s => s.PositionGenerated += null, testPosition);

            // Act
            var result = m_ViewModel.GeoPosition;

            // Assert
            Assert.That(result.Position.Latitude, Is.EqualTo(50.0));
            Assert.That(result.Position.Longitude, Is.EqualTo(10.0));
        }

        [Test]
        public void GeoAveragePosition_ShouldUpdate_WhenPositionsChange()
        {
            // Arrange
            var positions = new List<Position>
            {
                new Position(50.0, 10.0),
                new Position(60.0, 20.0),
                new Position(70.0, 30.0)
            };

            foreach (var position in positions)
            {
                m_SimulatorMock.Raise(s => s.PositionGenerated += null, new GeoPosition(position, DateTime.UtcNow));
            }

            // Act
            var result = m_ViewModel.GeoAveragePosition;

            // Assert
            Assert.That(result.AveragePosition.Latitude, Is.EqualTo(60.0));
            Assert.That(result.AveragePosition.Longitude, Is.EqualTo(20.0));
        }
    }
}
