namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Utilities for calculating checksums.
    /// </summary>
    public static class ChecksumUtility
    {
        /// <summary>
        /// Calculate the command checksum.
        /// </summary>
        /// <param name="payloadBytes">Payload bytes user for checksum calculation.</param>
        /// <returns>The calculated checksum.</returns>
        public static byte CalculateFrom(IEnumerable<byte> payloadBytes)
        {
            var checksum = ~payloadBytes.Sum(x => x);

            return (byte)checksum;
        }
    }
}
