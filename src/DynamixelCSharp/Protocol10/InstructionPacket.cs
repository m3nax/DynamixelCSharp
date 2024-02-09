using System.Collections;

namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Represents an instruction packet sent to a device.
    /// </summary>
    public record class InstructionPacket : IEnumerable<byte>
    {
        private const byte Header1 = 0xFF;
        private const byte Header2 = 0xFF;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionPacket"/> struct.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="instruction"></param>
        /// <param name="parameters"></param>
        public InstructionPacket(byte deviceId, byte instruction, params byte[] parameters)
        {
            DeviceId = deviceId;
            Instruction = instruction;
            Parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionPacket"/> struct.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="instruction"></param>
        /// <param name="address"></param>
        /// <param name="parameters"></param>
        public InstructionPacket(byte deviceId, byte instruction, byte address, params byte[] parameters)
        {
            DeviceId = deviceId;
            Instruction = instruction;
            Parameters = parameters.Prepend(address).ToArray();
        }

        /// <summary>
        /// Id of the device to send the instruction to.
        /// </summary>
        public byte DeviceId { get; private init; }

        /// <summary>
        /// Instruction to send to the device.
        /// </summary>
        public byte Instruction { get; private init; }

        /// <summary>
        /// Parameters of the instruction.
        /// </summary>
        public IReadOnlyList<byte> Parameters { get; private init; }

        /// <summary>
        /// Length of the instruction packet.
        /// Formula: 1 (instruction) + n (parameters) + 1 (checksum).
        /// </summary>
        public byte Length => (byte)(2 + Parameters.Count);

        /// <summary>
        /// Checksum of the instruction packet.
        /// </summary>
        public byte Checksum => ChecksumUtility.CalculateFrom(Packet);

        /// <summary>
        /// Return packet of the instruction packet.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<byte> Packet
        {
            get
            {
                yield return DeviceId;
                yield return Length;
                yield return Instruction;

                foreach (byte b in Parameters)
                {
                    yield return b;
                }
            }
        }

        /// <summary>
        /// Implicit conversion to byte array.
        /// </summary>
        /// <param name="i"></param>
        public static implicit operator byte[](InstructionPacket i)
            => ToByteArray(i);

        /// <summary>
        /// Convert instruction packet to byte array.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(InstructionPacket i)
            => i.ToArray();

        /// <inheritdoc/>
        public IEnumerator<byte> GetEnumerator()
        {
            yield return Header1;
            yield return Header2;

            foreach (byte b in Packet)
            {
                yield return b;
            }

            yield return Checksum;
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Header1;
            yield return Header2;

            foreach (byte b in Packet)
            {
                yield return b;
            }

            yield return Checksum;
        }
    }
}
