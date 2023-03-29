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
            var timeoutDatetime = DateTime.Now.Add(timeout);

            while (port.BytesToRead < bytesAmount)
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
