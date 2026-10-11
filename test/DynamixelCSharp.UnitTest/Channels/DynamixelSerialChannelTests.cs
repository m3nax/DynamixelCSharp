using DynamixelCSharp.Channels;

namespace DynamixelCSharp.UnitTest.Channels
{
    public class DynamixelSerialChannelTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_InvalidBaudRate_ThrowsArgumentOutOfRangeException(int baudRate)
        {
            // Arrange

            // Act
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new DynamixelSerialChannel("COM3", baudRate));

            // Assert
            Assert.Equal("baudRate", ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_InvalidBaudRateWithReadTimeout_ThrowsArgumentOutOfRangeException(int baudRate)
        {
            // Arrange

            // Act
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new DynamixelSerialChannel("COM3", baudRate, TimeSpan.FromMilliseconds(50)));

            // Assert
            Assert.Equal("baudRate", ex.ParamName);
        }

        [Fact]
        public void Constructor_ValidBaudRate_SetsProperties()
        {
            // Arrange

            // Act
            using var channel = new DynamixelSerialChannel("COM3", 57600);

            // Assert
            Assert.Equal("COM3", channel.PortName);
            Assert.Equal(57600, channel.BaudRate);
            Assert.Equal(TimeSpan.FromMilliseconds(50), channel.ReadTimeout);
        }
    }
}
