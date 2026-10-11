using System.IO.Ports;

namespace DynamixelCSharp.Channels
{
    internal static class SerialPortExtensions
    {
        /// <summary>
        /// Waits for a specific amount of data to be present in the read buffer.
        /// If the data is not present in the buffer within the timeout throw an <see cref="TimeoutException"/>.
        /// </summary>
        /// <param name="port">Serial port.</param>
        /// <param name="bytesAmount">Amount of bytes to wait.</param>
        /// <param name="timeout">Timeout.</param>
        public static void WaitForData(this SerialPort port, int bytesAmount, TimeSpan timeout)
        {
            WaitForData(() => port.BytesToRead, bytesAmount, timeout);
        }

        /// <summary>
        /// Waits for a specific amount of data to be available.
        /// If the data is not available within the timeout throw an <see cref="TimeoutException"/>.
        /// </summary>
        /// <param name="bytesAvailable">Function returning the amount of bytes currently available to read.</param>
        /// <param name="bytesAmount">Amount of bytes to wait.</param>
        /// <param name="timeout">Timeout.</param>
        internal static void WaitForData(Func<int> bytesAvailable, int bytesAmount, TimeSpan timeout)
        {
            var timeoutDatetime = DateTime.Now.Add(timeout);

            while (bytesAvailable() < bytesAmount)
            {
                if (DateTime.Now > timeoutDatetime)
                {
                    throw new TimeoutException("Timeout reached while waiting for data.");
                }

                Thread.Sleep(1);
            }
        }
    }
}
