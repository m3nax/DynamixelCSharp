using System.IO.Ports;

namespace DynamixelCSharp.Channels
{
    /// <summary>
    /// Represents a serial channel.
    /// </summary>
    public class DynamixelSerialChannel : IDynamixelChannel
    {
        /// <summary>
        /// Lock object for read/write operations.
        /// </summary>
        private readonly object comLock = new();

        /// <summary>
        /// Serial port used for communication.
        /// </summary>
        private readonly SerialPort serialPort = new SerialPort();

        private bool disposedValue;

        /// <summary>
        /// Create a new serial channel.
        /// </summary>
        /// <param name="portName">Name of serial port.</param>
        /// <param name="baudRate">Baud rate of serial port.</param>
        /// <exception cref="NotImplementedException"></exception>
        public DynamixelSerialChannel(string portName, int baudRate)
        {
            if (string.IsNullOrWhiteSpace(portName))
            {
                throw new ArgumentException($"'{nameof(portName)}' cannot be null or whitespace.", nameof(portName));
            }

            if (baudRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baudRate), $"'{nameof(baudRate)}' cannot be less or equal to zero.");
            }

            PortName = portName;
            BaudRate = baudRate;
        }

        /// <inheritdoc/>
        public DynamixelChannelStatus Status { get; protected set; }

        /// <summary>
        /// Serial port name.
        /// </summary>
        public string PortName { get; }

        /// <summary>
        /// Serial port baud rate.
        /// </summary>
        public int BaudRate { get; }

        /// <inheritdoc/>
        public void Close()
        {
            if (Status == DynamixelChannelStatus.Open)
            {
                lock (comLock)
                {
                    serialPort.Close();

                    Status = DynamixelChannelStatus.Closed;
                }
            }
        }

        /// <inheritdoc/>
        public void Open()
        {
            if (Status == DynamixelChannelStatus.Open)
            {
                return;
            }

            lock (comLock)
            {
                ConfigureAndOpenSerialPort();

                Status = DynamixelChannelStatus.Open;
            }
        }

        /// <inheritdoc/>
        public byte[] Send(byte[] command, int responseLength)
        {
            var response = new byte[responseLength];

            lock (comLock)
            {
                this.serialPort.Write(command, 0, command.Length);

                // TODO: improve data read
                serialPort.Read(response, 0, responseLength);
            }

            return response;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Close();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Configure the serial port for Dynamixel communication and open it.
        /// </summary>
        private void ConfigureAndOpenSerialPort()
        {
            serialPort.PortName = this.PortName;
            serialPort.BaudRate = this.BaudRate;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;

            serialPort.Open();
        }
    }
}
