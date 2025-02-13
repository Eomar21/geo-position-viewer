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
        private PositionSimulator m_Simulator;
        private PositionViewModel m_ViewModel;

        [SetUp]
        public void SetUp()
        {
            m_GeoPositionProcessorMock = new Mock<IGeoPositionProcessor>();
            m_Simulator = new PositionSimulator();

            m_GeoPositionProcessorMock.Setup(p => p.GetAveragePosition(It.IsAny<IEnumerable<Position>>()))
                .Returns((IEnumerable<Position> positions) =>
                {
                    var avgLat = positions.Average(p => p.Latitude);
                    var avgLon = positions.Average(p => p.Longitude);
                    return new GeoAveragePosition(new Position(avgLat, avgLon), positions.Count());
                });

            m_ViewModel = new PositionViewModel(m_GeoPositionProcessorMock.Object, m_Simulator);
        }

        [TearDown]
        public void TearDown()
        {
            m_GeoPositionProcessorMock = null;
            m_Simulator = null;
            m_ViewModel = null;
            m_Simulator?.Dispose();
        }

        [Test]
        public async Task GeoRawPosition_ShouldUpdate_OnPositionGenerated()
        {
            // Arrange
            var testPosition = new GeoPosition(new Position(50.0, 10.0), DateTime.UtcNow);

            SimulatePositionGeneration(testPosition);

            await Task.Delay(50);

            // Act
            var result = m_ViewModel.GeoRawPosition;

            // Assert
            Assert.That(result.Position.Latitude, Is.EqualTo(50.0));
            Assert.That(result.Position.Longitude, Is.EqualTo(10.0));
        }

        [Test]
        public async Task GeoPosition_ShouldUpdate_AfterThrottledInterval()
        {
            // Arrange
            var testPosition = new GeoPosition(new Position(50.0, 10.0), DateTime.UtcNow);

            SimulatePositionGeneration(testPosition);
            SimulatePositionGeneration(testPosition);
            await Task.Delay(2500);

            // Act
            var result = m_ViewModel.GeoPosition;

            // Assert
            Assert.That(result.Position.Latitude, Is.EqualTo(50.0));
            Assert.That(result.Position.Longitude, Is.EqualTo(10.0));
        }

        private void SimulatePositionGeneration(GeoPosition position)
        {
            m_Simulator.TriggerPositionGenerated(position);
        }
    }
}
