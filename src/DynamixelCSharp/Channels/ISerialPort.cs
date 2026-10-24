namespace DynamixelCSharp.Channels
{
    /// <summary>
    /// Serial port operations used by <see cref="DynamixelSerialChannel"/>.
    /// Allows the channel to be tested without a physical serial port.
    /// </summary>
    internal interface ISerialPort : IDisposable
    {
        /// <summary>
        /// Number of bytes available in the read buffer.
        /// </summary>
        int BytesToRead { get; }

        /// <summary>
        /// Discard all bytes currently in the read buffer.
        /// </summary>
        void DiscardInBuffer();

        /// <summary>
        /// Configure the serial port for Dynamixel communication and open it.
        /// </summary>
        /// <param name="portName">Name of serial port.</param>
        /// <param name="baudRate">Baud rate of serial port.</param>
        void Open(string portName, int baudRate);

        /// <summary>
        /// Close the serial port.
        /// </summary>
        void Close();

        /// <summary>
        /// Write bytes to the serial port.
        /// </summary>
        /// <param name="buffer">Buffer containing the bytes to write.</param>
        /// <param name="offset">Offset in the buffer of the first byte to write.</param>
        /// <param name="count">Number of bytes to write.</param>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// Read bytes from the serial port.
        /// </summary>
        /// <param name="buffer">Buffer that receives the bytes read.</param>
        /// <param name="offset">Offset in the buffer at which to start writing.</param>
        /// <param name="count">Maximum number of bytes to read.</param>
        /// <returns>Number of bytes read.</returns>
        int Read(byte[] buffer, int offset, int count);
    }
}
