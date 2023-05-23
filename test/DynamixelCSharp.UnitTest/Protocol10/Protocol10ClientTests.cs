using DynamixelCSharp.Protocol10;

namespace DynamixelCSharp.UnitTest.Protocol10
{
    public class Protocol10ClientTests
    {
        [Fact]
        public void Ctor_NullChannel_ThrowArgumentNullException()
        {
            // Arrange

            // Act
            var ctor = () => new Protocol10Client(null!);

            // Assert
            Assert.Throws<ArgumentNullException>(() => ctor());
        }
    }
}
