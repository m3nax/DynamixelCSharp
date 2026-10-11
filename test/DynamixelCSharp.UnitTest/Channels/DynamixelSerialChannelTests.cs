using System.Diagnostics;
using DynamixelCSharp.Channels;

namespace DynamixelCSharp.UnitTest.Channels
{
    public class DynamixelSerialChannelTests
    {
        public static TheoryData<TimeSpan> NonPositiveTimeouts() => new()
        {
            TimeSpan.Zero,
            TimeSpan.FromMilliseconds(-1),
        };

        [Fact]
        public void Ctor_DefaultTimeout_Is50Milliseconds()
        {
            // Arrange

            // Act
            using var channel = new DynamixelSerialChannel("COM1", 1000000);

            // Assert
            Assert.Equal(TimeSpan.FromMilliseconds(50), channel.ReadTimeout);
        }

        [Fact]
        public void Ctor_CustomTimeout_IsStored()
        {
            // Arrange
            var timeout = TimeSpan.FromMilliseconds(200);

            // Act
            using var channel = new DynamixelSerialChannel("COM1", 1000000, timeout);

            // Assert
            Assert.Equal(timeout, channel.ReadTimeout);
        }

        [Theory]
        [MemberData(nameof(NonPositiveTimeouts))]
        public void Ctor_NonPositiveTimeout_ThrowsArgumentOutOfRangeException(TimeSpan timeout)
        {
            // Arrange

            // Act
            var ctor = () => new DynamixelSerialChannel("COM1", 1000000, timeout);

            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(ctor);
            Assert.Equal("readTimeout", exception.ParamName);
        }

        [Fact]
        public void Send_ResponseArrivesWithinReadTimeout_ReturnsResponse()
        {
            // Arrange
            // The response arrives after the 50 ms default, so this only passes if Send waits for ReadTimeout.
            byte[] command = [0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFB];
            byte[] response = [0xFF, 0xFF, 0x01, 0x02, 0x00, 0xFC];
            var stopwatch = Stopwatch.StartNew();
            using var port = new FakeSerialPort(() => stopwatch.ElapsedMilliseconds >= 100 ? response.Length : 0, response);
            using var channel = new DynamixelSerialChannel("COM1", 1000000, TimeSpan.FromSeconds(1), port);

            // Act
            var result = channel.Send(command, response.Length);

            // Assert
            Assert.Equal(command, port.Written);
            Assert.Equal(response, result);
        }

        [Fact]
        public void Send_NoResponse_ThrowsTimeoutExceptionAfterReadTimeout()
        {
            // Arrange
            using var port = new FakeSerialPort(() => 0);
            using var channel = new DynamixelSerialChannel("COM1", 1000000, TimeSpan.FromMilliseconds(50), port);
            var stopwatch = Stopwatch.StartNew();

            // Act
            var send = () => channel.Send([0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFB], 6);

            // Assert
            Assert.Throws<TimeoutException>(send);
            Assert.InRange(stopwatch.ElapsedMilliseconds, 45, 500);
        }
    }
}
