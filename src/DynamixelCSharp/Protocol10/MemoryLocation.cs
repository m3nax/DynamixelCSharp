namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Represents a memory location.
    /// </summary>
    public record struct MemoryLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLocation"/> struct.
        /// </summary>
        /// <param name="address">Address of memory location.</param>
        /// <param name="accessMode">Access mode of memory location.</param>
        public MemoryLocation(byte address, AccessMode accessMode)
            : this(address, 0x01, accessMode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLocation"/> struct.
        /// </summary>
        /// <param name="address">Address of memory location.</param>
        /// <param name="length">Length of the memory location.</param>
        /// <param name="accessMode">Access mode of memory location.</param>
        public MemoryLocation(byte address, byte length, AccessMode accessMode)
        {
            Address = address;
            Length = length;
            AccessMode = accessMode;
        }

        /// <summary>
        /// Address of the memory location.
        /// </summary>
        public byte Address { get; }

        /// <summary>
        /// Length of the memory location.
        /// </summary>
        public byte Length { get; }

        /// <summary>
        /// Access mode of the memory location.
        /// </summary>
        public AccessMode AccessMode { get; }
    }
}
