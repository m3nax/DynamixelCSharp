using DynamixelCSharp.Channels;
using DynamixelCSharp.Exceptions;

namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Dynamixel protocol 1.0 client.
    /// </summary>
    public class Protocol10Client
    {
        private readonly byte deviceIdBroadcast = 0xFE;
        private readonly IDynamixelChannel dynamixelChannel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Protocol10Client"/> class.
        /// </summary>
        /// <param name="dynamixelChannel">Channel used for the communication.</param>
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

            ResponsePacket response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response.Error);
        }

        /// <summary>
        /// The Read method reads a memory location in a device and returns an array of bytes that
        /// represents the data read.
        /// </summary>
        /// <param name="deviceId">Device id.</param>
        /// <param name="location">Location of memory to read.</param>
        /// <returns>
        /// Returns the Parameters property of the response object which is an array of bytes that
        /// represents the data read from the device.
        /// </returns>
        public byte[] Read(byte deviceId, MemoryLocation location)
        {
            if (!location.AccessMode.HasFlag(AccessMode.Read))
            {
                throw new InvalidOperationException("Memory location isn't readable");
            }

            byte[] command = new InstructionPacket(deviceId, Instructions.Read, location.Address, location.Length);
            byte responseLength = (byte)(6 + location.Length);

            ResponsePacket response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response.Error);

            return response.Parameters;
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
                throw new DynamixelException("Memory location size is smaller than size of data to write");
            }

            if (!location.AccessMode.HasFlag(AccessMode.Write))
            {
                throw new InvalidOperationException("Memory location isn't writable");
            }

            byte[] command = new InstructionPacket(deviceId, Instructions.Write, location.Address, values);
            var responseLength = 6;

            ResponsePacket response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response.Error);
        }

        /// <summary>
        /// Write data in the given location.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="location"></param>
        /// <param name="values"></param>
        public void RegWrite(byte deviceId, MemoryLocation location, params byte[] values)
        {
            if (values.Length != location.Length)
            {
                throw new DynamixelException("Memory location size is smaller than size of data to write");
            }

            if (!location.AccessMode.HasFlag(AccessMode.Write))
            {
                throw new InvalidOperationException("Memory location isn't writable");
            }

            byte[] command = new InstructionPacket(deviceId, Instructions.RegWrite, location.Address, values);
            var responseLength = 6;

            ResponsePacket response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response.Error);
        }

        /// <summary>
        /// Execute the action instruction on the specified device id registered with <see cref="RegWrite(byte, MemoryLocation, byte[])"/> method.
        /// Note: no status packet is returned.
        /// </summary>
        public void Action()
        {
            byte[] command = new InstructionPacket(deviceIdBroadcast, Instructions.Action);
            var responseLength = 0;

            dynamixelChannel.Send(command, responseLength);
        }

        /// <summary>
        /// This instruction is to reset the Control Table of DYNAMIXEL to the factory default values.
        /// </summary>
        /// <param name="deviceId"></param>
        public void FactoryReset(byte deviceId)
        {
            byte[] command = new InstructionPacket(deviceId, Instructions.FactoryReset);
            byte responseLength = 6;

            ResponsePacket response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response.Error);
        }

        /// <summary>
        /// This instruction restarts DYNAMIXEL.
        /// Supported products : DYNAMIXEL MX-12W(V41), MX-28/64/106(V40), MX(2.0) and X Series(excluding XL-320).
        /// </summary>
        /// <param name="deviceId"></param>
        public void Reboot(byte deviceId)
        {
            byte[] command = new InstructionPacket(deviceId, Instructions.Reboot);
            byte responseLength = 6;

            ResponsePacket response = dynamixelChannel.Send(command, responseLength);

            ThrowIfStatusErrorOccurred(response.Error);
        }

        /// <summary>
        /// Throw an exception if an error occurred.
        /// </summary>
        /// <param name="errorByte"></param>
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
