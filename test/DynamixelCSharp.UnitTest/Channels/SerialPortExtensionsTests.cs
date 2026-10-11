using System.Diagnostics;
using DynamixelCSharp.Channels;

namespace DynamixelCSharp.UnitTest.Channels
{
    public class SerialPortExtensionsTests
    {
        [Fact]
        public void WaitForData_DataAlreadyAvailable_ReturnsWithoutException()
        {
            // Arrange
            using var port = new FakeSerialPort(() => 5);

            // Act
            var exception = Record.Exception(() => port.WaitForData(5, TimeSpan.FromMilliseconds(500)));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WaitForData_DataArrivesBeforeTimeout_DoesNotThrow()
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            using var port = new FakeSerialPort(() => stopwatch.ElapsedMilliseconds >= 20 ? 5 : 0);

            // Act
            var exception = Record.Exception(() => port.WaitForData(5, TimeSpan.FromSeconds(1)));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WaitForData_MaxValueTimeout_DataArrives_DoesNotThrow()
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            using var port = new FakeSerialPort(() => stopwatch.ElapsedMilliseconds >= 20 ? 5 : 0);

            // Act
            var exception = Record.Exception(() => port.WaitForData(5, TimeSpan.MaxValue));

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void WaitForData_NotEnoughDataWithinTimeout_ThrowsTimeoutExceptionAfterTimeout(int bytesAvailable)
        {
            // Arrange
            using var port = new FakeSerialPort(() => bytesAvailable);
            var stopwatch = Stopwatch.StartNew();

            // Act
            var action = () => port.WaitForData(5, TimeSpan.FromMilliseconds(50));

            // Assert
            Assert.Throws<TimeoutException>(action);
            Assert.True(stopwatch.ElapsedMilliseconds >= 45, $"Elapsed {stopwatch.ElapsedMilliseconds} ms, expected at least 45 ms.");
        }
    }
}
