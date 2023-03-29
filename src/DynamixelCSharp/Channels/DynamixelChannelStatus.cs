namespace DynamixelCSharp.Channels
{
    /// <summary>
    /// Represents the status of a Dynamixel communication channel.
    /// </summary>
    public enum DynamixelChannelStatus
    {
        /// <summary>
        /// Channel is closed.
        /// </summary>
        Closed,

        /// <summary>
        /// Channel is open.
        /// </summary>
        Open,

        /// <summary>
        /// Channel is faulted.
        /// </summary>
        Faulted,
    }
}
