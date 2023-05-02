using DynamixelCSharp.Protocol10;

namespace DynamixelCSharp.UnitTest.Protocol10
{
    public class ChecksumUtilityTests
    {
        /// <summary>
        /// Test checksum calculation.
        /// Checksum is computed by summing all bytes in the payload and then inverting the result.
        /// Payload is the array of bytes between the header and the checksum.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="expectedChecksum"></param>
        [Theory]
        // Ping command (https://emanual.robotis.com/docs/en/dxl/protocol1/#ping)
        [InlineData(new object[] { new byte[] { 0x01, 0x02, 0x01 }, 0xFB })]
        // Read command (https://emanual.robotis.com/docs/en/dxl/protocol1/#read)
        [InlineData(new object[] { new byte[] { 0x01, 0x04, 0x02, 0x2B, 0x01 }, 0xCC })]
        // Write command (https://emanual.robotis.com/docs/en/dxl/protocol1/#write)
        [InlineData(new object[] { new byte[] { 0xFE, 0x04, 0x03, 0x03, 0x01 }, 0xF6 })]
        // Reg Write command (https://emanual.robotis.com/docs/en/dxl/protocol1/#reg-write)
        [InlineData(new object[] { new byte[] { 0x01, 0x05, 0x04, 0x1E, 0xF4, 0x01 }, 0xE2 })]
        public void CalculateFrom_ArrayOfBytes_ReturnExpectedChecksum(byte[] payload, byte expectedChecksum)
        {
            // Arrange

            // Act
            var calculatedChecksum = ChecksumUtility.CalculateFrom(payload);

            // Assert
            Assert.Equal(expectedChecksum, calculatedChecksum);
        }
    }
}