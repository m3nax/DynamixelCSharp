namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Represents the instructions for the Dynamixel protocol 1.0.
    /// </summary>
    public static class Instructions
    {
        /// <summary>
        /// Instruction that checks whether the Packet has arrived to a device with the same ID as Packet ID.
        /// </summary>
        public const byte Ping = 0x01;

        /// <summary>
        /// Instruction to read data from the Device.
        /// </summary>
        public const byte Read = 0x02;

        /// <summary>
        /// Instruction to write data on the Device.
        /// </summary>
        public const byte Write = 0x03;

        /// <summary>
        /// Instruction that registers the Instruction Packet to a standby status; Packet is later executed through the Action instruction.
        /// </summary>
        public const byte RegWrite = 0x04;

        /// <summary>
        /// Instruction that executes the Packet that was registered beforehand using Reg Write.
        /// </summary>
        public const byte Action = 0x05;

        /// <summary>
        /// Instruction that resets the Control Table to its initial factory default settings.
        /// </summary>
        public const byte FactoryReset = 0x06;

        /// <summary>
        /// Instruction that reboots DYNAMIXEL (See supported products in the description).
        /// </summary>
        public const byte Reboot = 0x08;

        /// <summary>
        /// For multiple devices, Instruction to write data on the same Address with the same length at once.
        /// </summary>
        public const byte SyncWrite = 0x83;

        /// <summary>
        /// 	For multiple devices, Instruction to write data on different Addresses with different lengths at once (See supported products in the description).
        /// </summary>
        public const byte BulkRead = 0x92;
    }
}
