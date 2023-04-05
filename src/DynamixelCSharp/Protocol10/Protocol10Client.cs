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

            ThrowIfStatusErrorOccurred(response[4]);
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

            ThrowIfStatusErrorOccurred(response[4]);

            return response[5..6];
        }

        /// <summary>
        /// Write data in the given location.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="location"></param>
        /// <param name="values"></param>
        public void Write(byte deviceId, MemoryLocation location, params byte[] values)
        {
            if (values.Length != location.Length)
            {
                throw new Exception();
            }

            byte instruction = 0x03;
            byte length = (byte)(0x03 + location.Length);
            byte checksum = CalculateChecksum(deviceId, length, instruction, new byte[] { location.Address }.Concat(values).ToArray());
            byte responseLength = 6;

            byte[] command = new byte[] { Header1, Header2, deviceId, length, instruction, location.Address }
                .Concat(values)
                .Concat(new byte[] { checksum })
                .ToArray();

            var response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response[4]);
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

        /// <summary>
        /// Throw an exception if an error occurred.
        /// </summary>
        /// <param name="errorByte"></param>
        /// <returns></returns>
        private static void ThrowIfStatusErrorOccurred(byte errorByte)
        {
            if (errorByte == StatusErrors.None)
            {
                return;
            }

            if ((errorByte & StatusErrors.InputVoltageError) != 0)
            {
                throw new Exception("InputVoltageError");
            }
            else if ((errorByte & StatusErrors.AngleLimitError) != 0)
            {
                throw new Exception("AngleLimitError");
            }
            else if ((errorByte & StatusErrors.OverheatingError) != 0)
            {
                throw new Exception("OverheatingError");
            }
            else if ((errorByte & StatusErrors.RangeError) != 0)
            {
                throw new Exception("RangeError");
            }
            else if ((errorByte & StatusErrors.ChecksumError) != 0)
            {
                throw new Exception("ChecksumError");
            }
            else if ((errorByte & StatusErrors.OverloadError) != 0)
            {
                throw new Exception("OverloadError");
            }
            else if ((errorByte & StatusErrors.InstructionError) != 0)
            {
                throw new Exception("InstructionError");
            }
            else if ((errorByte & StatusErrors.Undefined) != 0)
            {
                throw new Exception("Undefined");
            }
        }
    }
}
