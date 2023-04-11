using System.Runtime.Serialization;

namespace DynamixelCSharp.Exceptions
{
    /// <summary>
    /// Base exception for all Dynamixel exceptions.
    /// </summary>
    public class DynamixelException : Exception
    {
        /// <inheritdoc/>
        public DynamixelException()
        {
        }

        /// <inheritdoc/>
        public DynamixelException(string? message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public DynamixelException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        protected DynamixelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
