namespace DynamixelCSharp.Protocol10
{
    public static class Checksum
    {
        /// <summary>
        /// Calculate the command checksum.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="length"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public static byte CalculateFrom(byte deviceId, byte length, byte instruction, params byte[] parameters)
        {
            var checksum = ~(deviceId + length + instruction + parameters.Sum(x => x));

            return (byte)checksum;
        }

        /// <summary>
        /// Calculate the command checksum.
        /// </summary>
        /// <param name="payloadBytes"></param>
        /// <returns></returns>
        public static byte CalculateFrom(IEnumerable<byte> payloadBytes)
        {
            var checksum = ~(payloadBytes.Sum(x => x));

            return (byte)checksum;
        }
    }
}
