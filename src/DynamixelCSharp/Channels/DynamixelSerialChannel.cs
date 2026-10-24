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
        private readonly ISerialPort serialPort;

        private bool disposedValue;

        /// <summary>
        /// Create a new serial channel.
        /// </summary>
        /// <param name="portName">Name of serial port.</param>
        /// <param name="baudRate">Baud rate of serial port.</param>
        /// <exception cref="NotImplementedException"></exception>
        public DynamixelSerialChannel(string portName, int baudRate)
            : this(portName, baudRate, TimeSpan.FromMilliseconds(50))
        {
        }

        /// <summary>
        /// Create a new serial channel.
        /// </summary>
        /// <param name="portName">Name of serial port.</param>
        /// <param name="baudRate">Baud rate of serial port.</param>
        /// <param name="readTimeout">Read timeout.</param>
        /// <exception cref="NotImplementedException"></exception>
        public DynamixelSerialChannel(string portName, int baudRate, TimeSpan readTimeout)
            : this(portName, baudRate, readTimeout, serialPort: null)
        {
        }

        /// <summary>
        /// Create a new serial channel that communicates through the given serial port.
        /// </summary>
        /// <param name="portName">Name of serial port.</param>
        /// <param name="baudRate">Baud rate of serial port.</param>
        /// <param name="readTimeout">Read timeout.</param>
        /// <param name="serialPort">Serial port to use, or <see langword="null"/> to use a <see cref="System.IO.Ports.SerialPort"/>.</param>
        internal DynamixelSerialChannel(string portName, int baudRate, TimeSpan readTimeout, ISerialPort? serialPort)
        {
            if (string.IsNullOrWhiteSpace(portName))
            {
                throw new ArgumentException($"'{nameof(portName)}' cannot be null or whitespace.", nameof(portName));
            }

            if (baudRate <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baudRate), $"'{nameof(baudRate)}' cannot be less or equal to zero.");
            }

            if (readTimeout <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(readTimeout), $"'{nameof(readTimeout)}' cannot be less or equal to zero.");
            }

            PortName = portName;
            BaudRate = baudRate;
            ReadTimeout = readTimeout;

            // Created after validation, so an invalid argument does not leak a serial port.
            this.serialPort = serialPort ?? new SerialPortAdapter();
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

        /// <summary>
        /// Read timeout.
        /// </summary>
        public TimeSpan ReadTimeout { get; }

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
                serialPort.Open(this.PortName, this.BaudRate);

                Status = DynamixelChannelStatus.Open;
            }
        }

        /// <inheritdoc/>
        public byte[] Send(byte[] command, int responseLength)
        {
            ArgumentNullException.ThrowIfNull(command);

            var response = new byte[responseLength];

            // If channel is not open then open it.
            if (Status != DynamixelChannelStatus.Open)
            {
                this.Open();
            }

            lock (comLock)
            {
                // Drop bytes left over from a previous command (for example a reply that arrived
                // after a TimeoutException). Otherwise they would be returned as this command's response.
                serialPort.DiscardInBuffer();

                this.serialPort.Write(command, 0, command.Length);

                serialPort.WaitForData(responseLength, this.ReadTimeout);

                serialPort.Read(response, 0, responseLength);
            }

            return response;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the serial port.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Close();
                    serialPort.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
