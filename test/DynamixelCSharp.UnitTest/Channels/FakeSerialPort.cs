using DynamixelCSharp.Channels;

namespace DynamixelCSharp.UnitTest.Channels
{
    /// <summary>
    /// In-memory <see cref="ISerialPort"/> used to test channels without hardware.
    /// </summary>
    internal sealed class FakeSerialPort : ISerialPort
    {
        private readonly Func<int> bytesToRead;
        private readonly byte[] response;

        /// <summary>
        /// Create a new fake serial port.
        /// </summary>
        /// <param name="bytesToRead">Function returning the number of bytes available in the read buffer.</param>
        /// <param name="response">Bytes returned by <see cref="Read"/>.</param>
        public FakeSerialPort(Func<int> bytesToRead, byte[]? response = null)
        {
            this.bytesToRead = bytesToRead;
            this.response = response ?? [];
        }

        /// <inheritdoc/>
        public int BytesToRead => bytesToRead();

        /// <summary>
        /// Bytes passed to the last <see cref="Write"/> call.
        /// </summary>
        public byte[] Written { get; private set; } = [];

        /// <inheritdoc/>
        public void Open(string portName, int baudRate)
        {
        }

        /// <inheritdoc/>
        public void Close()
        {
        }

        /// <inheritdoc/>
        public void Write(byte[] buffer, int offset, int count) => Written = buffer[offset..(offset + count)];

        /// <inheritdoc/>
        public int Read(byte[] buffer, int offset, int count)
        {
            var read = Math.Min(count, response.Length);
            Array.Copy(response, 0, buffer, offset, read);
            return read;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
