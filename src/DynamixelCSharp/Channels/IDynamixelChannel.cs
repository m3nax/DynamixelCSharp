namespace DynamixelCSharp.Channels
{
    /// <summary>
    /// Interface represents a Dynamixel communication channel.
    /// </summary>
    public interface IDynamixelChannel : IDisposable
    {
        /// <summary>
        /// Gets the channel status.
        /// </summary>
        DynamixelChannelStatus Status { get; }

        /// <summary>
        /// Close the channel.
        /// If the channel is already closed, this method does nothing.
        /// </summary>
        void Close();

        /// <summary>
        /// Open the channel. 
        /// If the channel is already open, this method does nothing.
        /// If the channel is faulted or closed, this method will try to open the channel.
        /// </summary>
        void Open();

        /// <summary>
        /// Send a command and read the response.
        /// </summary>
        /// <param name="command">Command to send.</param>
        /// <param name="responseLength">Number of bytes returned by the command.</param>
        /// <returns>Returns the amount of byte requested.</returns>
        byte[] Send(byte[] command, int responseLength);
    }
}
