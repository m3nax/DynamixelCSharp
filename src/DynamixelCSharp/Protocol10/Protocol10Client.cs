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
            byte checksum = (byte)~(deviceId + length + instruction);
            byte responseLength = 6;

            byte[] command = new byte[] { Header1, Header2, deviceId, length, instruction, checksum };

            var response = dynamixelChannel.Send(command, responseLength);

            if (response[4] != 0)
            {
                throw new Exception();
            }
        }
    }
}
