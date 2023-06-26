using System.Runtime.Serialization;

namespace DynamixelCSharp.Exceptions
{
    /// <summary>
    /// Base exception for all Dynamixel exceptions.
    /// </summary>
    public class DynamixelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamixelException"/> class.
        /// </summary>
        public DynamixelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamixelException"/> class.
        /// </summary>
        public DynamixelException(string? message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamixelException"/> class.
        /// </summary>
        public DynamixelException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamixelException"/> class.
        /// </summary>
        protected DynamixelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
