namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Represents a memory location.
    /// </summary>
    public struct MemoryLocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLocation"/> struct.
        /// </summary>
        /// <param name="address">Address of memory location.</param>
        public MemoryLocation(byte address)
            : this(address, 0x01)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLocation"/> struct.
        /// </summary>
        /// <param name="address">Address of memory location.</param>
        /// <param name="length">Length of the memory location.</param>
        public MemoryLocation(byte address, byte length)
        {
            Address = address;
            Length = length;
        }

        /// <summary>
        /// Address of the memory location.
        /// </summary>
        public byte Address { get; }

        /// <summary>
        /// Length of the memory location.
        /// </summary>
        public byte Length { get; }
    }
}
