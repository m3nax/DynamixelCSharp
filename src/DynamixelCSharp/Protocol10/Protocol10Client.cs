using DynamixelCSharp.Channels;
using DynamixelCSharp.Exceptions;

namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Dynamixel protocol 1.0 client.
    /// </summary>
    public class Protocol10Client
    {
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
            byte[] command = new InstructionPacket(deviceId, Instructions.Ping);
            byte responseLength = 6;

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
            if (!location.AccessMode.HasFlag(AccessMode.Read))
            {
                throw new InvalidOperationException("Memory location isn't readable");
            }

            byte[] command = new InstructionPacket(deviceId, Instructions.Read, location.Address, location.Length);
            byte responseLength = 7;

            var response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response[4]);

            // TODO: handle 1 byte response and 2 byte response
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
                throw new Exception("Memory location size is smaller than size of data to write");
            }

            if (!location.AccessMode.HasFlag(AccessMode.Write))
            {
                throw new InvalidOperationException("Memory location isn't writable");
            }

            byte[] command = new InstructionPacket(deviceId, Instructions.Write, location.Address, values);
            var responseLength = 6;

            var response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response[4]);
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
                throw new DynamixelException("InputVoltageError");
            }
            else if ((errorByte & StatusErrors.AngleLimitError) != 0)
            {
                throw new DynamixelException("AngleLimitError");
            }
            else if ((errorByte & StatusErrors.OverheatingError) != 0)
            {
                throw new DynamixelException("OverheatingError");
            }
            else if ((errorByte & StatusErrors.RangeError) != 0)
            {
                throw new DynamixelException("RangeError");
            }
            else if ((errorByte & StatusErrors.ChecksumError) != 0)
            {
                throw new DynamixelException("ChecksumError");
            }
            else if ((errorByte & StatusErrors.OverloadError) != 0)
            {
                throw new DynamixelException("OverloadError");
            }
            else if ((errorByte & StatusErrors.InstructionError) != 0)
            {
                throw new DynamixelException("InstructionError");
            }
            else if ((errorByte & StatusErrors.Undefined) != 0)
            {
                throw new DynamixelException("Undefined");
            }
        }
    }
}
