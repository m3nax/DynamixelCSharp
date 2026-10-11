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
            Func<int> bytesAvailable = () => 5;

            // Act
            var exception = Record.Exception(() => SerialPortExtensions.WaitForData(bytesAvailable, 5, TimeSpan.FromMilliseconds(500)));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WaitForData_DataArrivesBeforeTimeout_DoesNotThrow()
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            Func<int> bytesAvailable = () => stopwatch.ElapsedMilliseconds >= 20 ? 5 : 0;

            // Act
            var exception = Record.Exception(() => SerialPortExtensions.WaitForData(bytesAvailable, 5, TimeSpan.FromSeconds(1)));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WaitForData_NoDataWithinTimeout_ThrowsTimeoutException()
        {
            // Arrange
            Func<int> bytesAvailable = () => 0;

            // Act
            var action = () => SerialPortExtensions.WaitForData(bytesAvailable, 5, TimeSpan.FromMilliseconds(50));

            // Assert
            Assert.Throws<TimeoutException>(action);
        }

        [Fact]
        public void WaitForData_NoData_WaitsAtLeastTimeout()
        {
            // Arrange
            Func<int> bytesAvailable = () => 0;
            var stopwatch = Stopwatch.StartNew();

            // Act
            Assert.Throws<TimeoutException>(() => SerialPortExtensions.WaitForData(bytesAvailable, 5, TimeSpan.FromMilliseconds(50)));
            stopwatch.Stop();

            // Assert
            Assert.True(stopwatch.ElapsedMilliseconds >= 45, $"Elapsed {stopwatch.ElapsedMilliseconds} ms, expected at least 45 ms.");
        }

        [Fact]
        public void WaitForData_InsufficientData_ThrowsTimeoutException()
        {
            // Arrange
            Func<int> bytesAvailable = () => 3;

            // Act
            var action = () => SerialPortExtensions.WaitForData(bytesAvailable, 5, TimeSpan.FromMilliseconds(50));

            // Assert
            Assert.Throws<TimeoutException>(action);
        }
    }
}
