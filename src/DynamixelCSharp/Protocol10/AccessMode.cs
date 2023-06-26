namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Represents the access mode of a memory location.
    /// </summary>
    [Flags]
    public enum AccessMode
    {
        /// <summary>
        /// The memory location is read-only.
        /// </summary>
        Read = 1,

        /// <summary>
        /// The memory location is write-only.
        /// </summary>
        Write = 2,

        /// <summary>
        /// The memory location is read-write.
        /// </summary>
        ReadWrite = Read | Write,
    }
}
