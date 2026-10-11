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
            var channel = new DynamixelSerialChannel("COM1", 1000000);

            // Assert
            Assert.Equal(TimeSpan.FromMilliseconds(50), channel.ReadTimeout);
        }

        [Fact]
        public void Ctor_CustomTimeout_IsStored()
        {
            // Arrange
            var timeout = TimeSpan.FromMilliseconds(200);

            // Act
            var channel = new DynamixelSerialChannel("COM1", 1000000, timeout);

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
    }
}
