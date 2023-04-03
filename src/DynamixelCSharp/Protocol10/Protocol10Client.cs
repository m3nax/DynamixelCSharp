using DynamixelCSharp.Channels;

namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Dynamixel protocol 1.0 client.
    /// </summary>
    public class Protocol10Client
    {
        private const byte Header1 = 0xFF;
        private const byte Header2 = 0xFF;


        private readonly IDynamixelChannel dynamixelChannel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Protocol10Client"/> class.
        /// </summary>
        /// <param name="dynamixelChannel"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Protocol10Client(IDynamixelChannel dynamixelChannel)
        {
            this.dynamixelChannel = dynamixelChannel ?? throw new ArgumentNullException(nameof(dynamixelChannel));
        }

        /// <summary>
        /// Execute the ping instruction on the specified device id.
        /// </summary>
        /// <param name="deviceId">Device id.</param>
        public void Ping(byte deviceId)
        {
            byte instruction = 0x01;
            byte length = 0x02;
            byte checksum = CalculateChecksum(deviceId, length, instruction);
            byte responseLength = 6;

            byte[] command = new byte[] { Header1, Header2, deviceId, length, instruction, checksum };

            var response = dynamixelChannel.Send(command, responseLength);

            // Check for errors
            if (response[4] != 0)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Read a memory location in the device.
        /// </summary>
        /// <param name="deviceId">Device id.</param>
        /// <param name="location">Location of memory to read.</param>
        public byte[] Read(byte deviceId, MemoryLocation location)
        {
            byte instruction = 0x02;
            byte length = 0x04;
            byte checksum = CalculateChecksum(deviceId, length, instruction, location.Address, location.Length);
            byte responseLength = 7;

            byte[] command = new byte[] { Header1, Header2, deviceId, length, instruction, location.Address, location.Length, checksum };

            var response = dynamixelChannel.Send(command, responseLength);

            // Check for errors
            if (response[4] != 0)
            {
                throw new Exception();
            }

            return response[5..6];
        }

        /// <summary>
        /// Calculate the command checksum
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="length"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        private static byte CalculateChecksum(byte deviceId, byte length, byte instruction, params byte[] parameters)
        {
            var checksum = ~(deviceId + length + instruction + parameters.Sum(x => x));

            return (byte)checksum;
        }
    }
}
