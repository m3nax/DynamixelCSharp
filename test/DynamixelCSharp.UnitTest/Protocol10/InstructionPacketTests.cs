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
            var expectedBytes = new byte[] { 0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFB };

            // Act
            var instructionPacket = new InstructionPacket(deviceId, instruction, parameters);

            // Assert
            Assert.Equal(expectedBytes, instructionPacket);
            Assert.Equal(deviceId, instructionPacket.DeviceId);
            Assert.Equal(instruction, instructionPacket.Instruction);
        }

        [Fact]
        public void Ctor_InstructionPacketMultipleParameters_CorrectlyCreateBytes()
        {
            // Arrange
            // Read Read Present Temperature, which is located at the address 43(0x2B).
            // DOC: https://emanual.robotis.com/docs/en/dxl/protocol1/#id-1-instruction-packet.
            byte deviceId = 0x01;
            byte instruction = 0x02;
            byte address = 0x2B;
            var parameters = new byte[] { 0x01 };
            var expectedBytes = new byte[] { 0xFF, 0xFF, 0x01, 0x04, 0x02, 0x2B, 0x01, 0xCC };

            // Act
            var instructionPacket = new InstructionPacket(deviceId, instruction, address, parameters);

            // Assert
            Assert.Equal(expectedBytes, instructionPacket);
            Assert.Equal(deviceId, instructionPacket.DeviceId);
            Assert.Equal(instruction, instructionPacket.Instruction);
        }
    }
}
