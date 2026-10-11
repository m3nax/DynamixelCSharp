using System.IO.Ports;

namespace DynamixelCSharp.Channels
{
    /// <summary>
    /// <see cref="ISerialPort"/> implementation backed by a <see cref="SerialPort"/>.
    /// </summary>
    internal sealed class SerialPortAdapter : ISerialPort
    {
        /// <summary>
        /// Serial port used for communication.
        /// </summary>
        private readonly SerialPort serialPort = new();

        /// <inheritdoc/>
        public int BytesToRead => serialPort.BytesToRead;

        /// <inheritdoc/>
        public void Open(string portName, int baudRate)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            serialPort.Open();
        }

        /// <inheritdoc/>
        public void Close() => serialPort.Close();

        /// <inheritdoc/>
        public void Write(byte[] buffer, int offset, int count) => serialPort.Write(buffer, offset, count);

        /// <inheritdoc/>
        public int Read(byte[] buffer, int offset, int count) => serialPort.Read(buffer, offset, count);

        /// <inheritdoc/>
        public void Dispose() => serialPort.Dispose();
    }
}
