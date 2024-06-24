using DynamixelCSharp.Protocol10;

namespace DynamixelCSharp.UnitTest.Protocol10
{
    public class InstructionPacketTests
    {
        [Fact]
        public void Ctor_InstructionPacketWithoutParameter_CorrectlyCreateBytes()
        {
            // Arrange
            // Smaller instruction packet is ping, which is composed of 6 bytes, and has no parameters.
            // DOC: https://emanual.robotis.com/docs/en/dxl/protocol1/#id-1-instruction-packet.
            byte deviceId = 0x01;
            byte instruction = 0x01;
            var parameters = Array.Empty<byte>();
            var expectedBytes = new byte[] { 0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFD };

            // Act
            var instructionPacket = new InstructionPacket(deviceId, instruction, parameters);

            // Assert
            Assert.Equal(expectedBytes, instructionPacket);
        }
    }
}
