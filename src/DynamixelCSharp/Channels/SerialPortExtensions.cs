using System.Diagnostics;

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
        public static void WaitForData(this ISerialPort port, int bytesAmount, TimeSpan timeout)
        {
            // Stopwatch is monotonic: unlike DateTime.Now it is not affected by clock or DST changes,
            // and it does not overflow for very large timeouts.
            var stopwatch = Stopwatch.StartNew();

            while (port.BytesToRead < bytesAmount)
            {
                if (stopwatch.Elapsed > timeout)
                {
                    throw new TimeoutException("Timeout reached while waiting for data.");
                }

                Thread.Sleep(1);
            }
        }
    }
}
