using DynamixelCSharp.Channels;

namespace DynamixelCSharp.UnitTest.Channels
{
    /// <summary>
    /// In-memory <see cref="ISerialPort"/> used to test channels without hardware.
    /// </summary>
    internal sealed class FakeSerialPort : ISerialPort
    {
        private readonly Func<int>? bytesToRead;
        private readonly byte[] response;
        private readonly Queue<byte> buffer = new();

        /// <summary>
        /// Create a new fake serial port in buffered mode: bytes enqueued with
        /// <see cref="EnqueueResponse"/> are returned by <see cref="Read"/> and
        /// counted by <see cref="BytesToRead"/>.
        /// </summary>
        public FakeSerialPort()
        {
            this.response = [];
        }

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
        public int BytesToRead => bytesToRead?.Invoke() ?? buffer.Count;

        /// <summary>
        /// Bytes passed to the last <see cref="Write"/> call.
        /// </summary>
        public byte[] Written { get; private set; } = [];

        /// <summary>
        /// Invoked at the end of <see cref="Write"/> with the written bytes,
        /// so a test can enqueue the reply to a command.
        /// </summary>
        public Action<byte[]>? OnWrite { get; set; }

        /// <summary>
        /// Number of times <see cref="DiscardInBuffer"/> has been called.
        /// </summary>
        public int DiscardInBufferCalls { get; private set; }

        /// <summary>
        /// Append bytes to the read buffer (buffered mode).
        /// </summary>
        /// <param name="bytes">Bytes to enqueue.</param>
        public void EnqueueResponse(byte[] bytes)
        {
            foreach (var b in bytes)
            {
                buffer.Enqueue(b);
            }
        }

        /// <inheritdoc/>
        public void DiscardInBuffer()
        {
            DiscardInBufferCalls++;
            buffer.Clear();
        }

        /// <inheritdoc/>
        public void Open(string portName, int baudRate)
        {
        }

        /// <inheritdoc/>
        public void Close()
        {
        }

        /// <inheritdoc/>
        public void Write(byte[] buffer, int offset, int count)
        {
            Written = buffer[offset..(offset + count)];
            OnWrite?.Invoke(Written);
        }

        /// <inheritdoc/>
        public int Read(byte[] buffer, int offset, int count)
        {
            if (bytesToRead != null)
            {
                var read = Math.Min(count, response.Length);
                Array.Copy(response, 0, buffer, offset, read);
                return read;
            }

            var dequeued = Math.Min(count, this.buffer.Count);
            for (var i = 0; i < dequeued; i++)
            {
                buffer[offset + i] = this.buffer.Dequeue();
            }

            return dequeued;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
