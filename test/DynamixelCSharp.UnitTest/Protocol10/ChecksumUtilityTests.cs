using DynamixelCSharp.Protocol10;

namespace DynamixelCSharp.UnitTest.Protocol10
{
    public class ChecksumUtilityTests
    {
        /// <summary>
        /// Test the checksum calculation.
        /// Checksum is computed by summing all bytes in the packet and then inverting the result.
        /// Packet is the array of bytes between the header and the checksum.
        /// </summary>
        /// <param name="packet"></param>
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

        // Action command (https://emanual.robotis.com/docs/en/dxl/protocol1/#action)
        [InlineData(new object[] { new byte[] { 0xFE, 0x02, 0x05 }, 0xFA })]

        // Factory reset command (https://emanual.robotis.com/docs/en/dxl/protocol1/#factory-reset)
        [InlineData(new object[] { new byte[] { 0x00, 0x02, 0x06 }, 0xF7 })]

        // Reboot command (https://emanual.robotis.com/docs/en/dxl/protocol1/#reboot)
        [InlineData(new object[] { new byte[] { 0x01, 0x02, 0x08 }, 0xF4 })]

        // Reboot command (https://emanual.robotis.com/docs/en/dxl/protocol1/#sync-write)
        [InlineData(new object[] { new byte[] { 0xFE, 0x0E, 0x83, 0x1E, 0x04, 0x00, 0x10, 0x00, 0x50, 0x01, 0x01, 0x20, 0x02, 0x60, 0x03 }, 0x67 })]

        // Reboot command (https://emanual.robotis.com/docs/en/dxl/protocol1/#bulk-read)
        [InlineData(new object[] { new byte[] { 0xFE, 0x09, 0x92, 0x00, 0x02, 0x01, 0x1E, 0x02, 0x02, 0x24 }, 0x1D })]
        public void CalculateFrom_ArrayOfBytes_ReturnExpectedChecksum(byte[] packet, byte expectedChecksum)
        {
            // Arrange

            // Act
            var calculatedChecksum = ChecksumUtility.CalculateFrom(packet);

            // Assert
            Assert.Equal(expectedChecksum, calculatedChecksum);
        }
    }
}