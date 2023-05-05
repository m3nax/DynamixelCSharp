﻿namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Represents a response packet returned by a device.
    /// </summary>
    public record class ResponsePacket
    {
        private readonly byte[] responseBytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponsePacket"/> struct.
        /// </summary>
        /// <param name="responseBytes"></param>
        public ResponsePacket(byte[] responseBytes)
        {
            if (responseBytes.Length < 6)
            {
                throw new ArgumentOutOfRangeException(nameof(responseBytes), $"The length of the parameter  {nameof(responseBytes)} must be greater or equal to 6");
            }

            this.responseBytes = responseBytes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponsePacket"/> struct.
        /// </summary>
        /// <param name="responseBytes"></param>
        public ResponsePacket(IEnumerable<byte> responseBytes)
            : this(responseBytes.ToArray())
        {
        }

        /// <summary>
        /// Id of the device that sent the packet.
        /// </summary>
        public byte DeviceId => responseBytes[2];

        /// <summary>
        /// Length of the response packet.
        /// </summary>
        public byte Length => responseBytes[3];

        /// <summary>
        /// Length of the response packet.
        /// </summary>
        public byte Error => responseBytes[4];

        /// <summary>
        /// Checksum of the response packet.
        /// </summary>
        public byte Checksum => responseBytes[responseBytes.Length - 1];

        /// <summary>
        /// Parameters of the response packet.
        /// </summary>
        public byte[] Parameters => responseBytes[5..(responseBytes.Length - 1)];

        /// <summary>
        /// Implicit conversion from byte array to responsePacket.
        /// </summary>
        /// <param name="bytes"></param>
        public static implicit operator ResponsePacket(byte[] bytes) => new ResponsePacket(bytes);
    }
}