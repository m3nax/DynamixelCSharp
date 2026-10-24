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

        [Fact]
        public void Constructor_CustomTimeout_IsStored()
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
        public void Constructor_NonPositiveTimeout_ThrowsArgumentOutOfRangeException(TimeSpan timeout)
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

        [Fact]
        public void Send_AfterTimeout_LateReplyIsDiscarded_ReturnsOwnResponse()
        {
            // Arrange
            byte[] command1 = [0xFF, 0xFF, 0x01, 0x04, 0x02, 0x2B, 0x01, 0xCC]; // read temperature
            byte[] reply1 = [0xFF, 0xFF, 0x01, 0x03, 0x00, 0x20, 0xDB];
            byte[] command2 = [0xFF, 0xFF, 0x01, 0x04, 0x02, 0x24, 0x02, 0xD2]; // read position
            byte[] reply2 = [0xFF, 0xFF, 0x01, 0x04, 0x00, 0x00, 0x02, 0xF8];
            using var port = new FakeSerialPort();
            using var channel = new DynamixelSerialChannel("COM1", 1000000, TimeSpan.FromMilliseconds(50), port);
            Assert.Throws<TimeoutException>(() => channel.Send(command1, reply1.Length));
            port.EnqueueResponse(reply1);                      // late reply to command1 lands in the buffer
            port.OnWrite = _ => port.EnqueueResponse(reply2);  // servo answers command2

            // Act
            var result = channel.Send(command2, reply2.Length);

            // Assert
            Assert.Equal(command2, port.Written);
            Assert.Equal(reply2, result);
            Assert.Equal(0, port.BytesToRead);
        }

        [Fact]
        public void Send_PartialStaleBytesInBuffer_ReturnsOwnResponse()
        {
            // Arrange
            byte[] command2 = [0xFF, 0xFF, 0x01, 0x04, 0x02, 0x24, 0x02, 0xD2]; // read position
            byte[] reply2 = [0xFF, 0xFF, 0x01, 0x04, 0x00, 0x00, 0x02, 0xF8];
            using var port = new FakeSerialPort();
            using var channel = new DynamixelSerialChannel("COM1", 1000000, TimeSpan.FromMilliseconds(50), port);
            port.EnqueueResponse([0xFF, 0xFF, 0x01]);          // partial stale packet left in the buffer
            port.OnWrite = _ => port.EnqueueResponse(reply2);  // servo answers command2

            // Act
            var result = channel.Send(command2, reply2.Length);

            // Assert
            Assert.Equal(reply2, result);
        }

        [Fact]
        public void Send_DiscardsInputBufferBeforeWrite()
        {
            // Arrange
            byte[] reply = [0xFF, 0xFF, 0x01, 0x02, 0x00, 0xFC];
            using var port = new FakeSerialPort();
            using var channel = new DynamixelSerialChannel("COM1", 1000000, TimeSpan.FromMilliseconds(50), port);
            port.OnWrite = _ => port.EnqueueResponse(reply);

            // Act
            channel.Send([0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFB], 6);

            // Assert
            Assert.Equal(1, port.DiscardInBufferCalls);
        }
    }
}
