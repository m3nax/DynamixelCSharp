namespace DynamixelCSharp.Channels
{
    /// <summary>
    /// Interface represents a Dynamixel communication channel.
    /// </summary>
    public interface IDynamixelChannel
    {
        /// <summary>
        /// Gets the channel status.
        /// </summary>
        DynamixelChannelStatus Status { get; }

        /// <summary>
        /// Open the channel. 
        /// If the channel is already open, this method does nothing.
        /// If the channel is faulted or closed, this method will try to open the channel.
        /// </summary>
        void Open();

        /// <summary>
        /// Close the channel.
        /// If the channel is already closed, this method does nothing.
        /// </summary>
        void Close();

        /// <summary>
        /// Write a command to the channel.
        /// </summary>
        /// <param name="command">Command to write.</param>
        void Write(byte[] command);

        /// <summary>
        /// Read the data returned by a read command.
        /// </summary>
        /// <param name="command">Read command.</param>
        /// <param name="count">Number of byte to read.</param>
        /// <returns></returns>
        byte[] Read(byte[] command, int count);
    }
}
