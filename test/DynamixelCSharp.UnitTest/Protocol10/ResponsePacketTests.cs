using DynamixelCSharp.Exceptions;
using DynamixelCSharp.Protocol10;

namespace DynamixelCSharp.UnitTest.Protocol10
{
    public class ResponsePacketTests
    {
        [Fact]
        public void Ctor_LengthLessThanSix_ThrowArgumentOutOfRangeException()
        {
            // Arrange
            // Smaller result is ping response, which is 6 bytes.
            // In this case, we are testing for 5 bytes, removing the checksum.
            // Doc: https://emanual.robotis.com/docs/en/dxl/protocol1/#id-1-status-packet.
            var responseBytes = new byte[] { 0xFF, 0xFF, 0x01, 0x02, 0x00, };

            // Act
            var ctor = () => new ResponsePacket(responseBytes);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ctor());
        }

        [Fact]
        public void Ctor_InvalidChecksum_ThrowDynamixelException()
        {
            // Arrange
            // Checksum is the last byte of the packet. The expected value is the sum of all bytes,
            // except the header and checksum. In this case the correct checksum is 0xDB.
            // DOC: https://emanual.robotis.com/docs/en/dxl/protocol1/#id-1-status-packet-1
            var responseBytes = new byte[] { 0xFF, 0xFF, 0x01, 0x03, 0x00, 0x20, 0xDA };


            // Act
            var ctor = () => new ResponsePacket(responseBytes);

            //Assert
            Assert.Throws<DynamixelException>(() => ctor());
        }

        [Fact]
        public void Ctor_ValidResponse_CorrectlyParsed()
        {
            // Arrange
            // DOC: https://emanual.robotis.com/docs/en/dxl/protocol1/#id-1-status-packet-1
            var responseBytes = new byte[] { 0xFF, 0xFF, 0x01, 0x03, 0x10, 0x20, 0xDB };
            var expectedDeviceId = 0x01;
            var expectedError = 0x10;
            var expectedParameters = new byte[] { 0x20 };

            // Act
            var responsePacket = new ResponsePacket(responseBytes);

            // Assert
            Assert.Equal(expectedDeviceId, responsePacket.DeviceId);
            Assert.Equal(expectedError, responsePacket.Error);
            Assert.Equal(expectedParameters, responsePacket.Parameters);
        }
    }
}
