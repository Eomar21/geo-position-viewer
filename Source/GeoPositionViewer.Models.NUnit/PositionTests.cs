namespace GeoPositionViewer.Models.NUnit
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Constructor_SetsLatitudeAndLongitude()
        {
            // Arrange
            double latitude = 10.0;
            double longitude = 20.0;

            // Act
            var position = new Position(latitude, longitude);

            // Assert
            Assert.That(position.Latitude, Is.EqualTo(latitude));
            Assert.That(position.Longitude, Is.EqualTo(longitude));
        }

        [Test]
        public void Empty_ReturnsZeroedPosition()
        {
            // Arrange & Act
            var empty = Position.Empty;

            // Assert
            Assert.That(empty.Latitude, Is.EqualTo(0.0));
            Assert.That(empty.Longitude, Is.EqualTo(0.0));
        }
    }
}