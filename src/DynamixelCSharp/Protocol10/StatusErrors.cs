namespace DynamixelCSharp.Protocol10
{
    /// <summary>
    /// Status errors.
    /// </summary>
    public static class StatusErrors
    {
        /// <summary>
        /// No error.
        /// </summary>
        public const byte None = 0;

        /// <summary>
        /// When the applied voltage is out of the range of operating voltage set in the Control table, it is as 1.
        /// </summary>
        public const byte InputVoltageError = 1;

        /// <summary>
        /// When Goal Position is written out of the range from CW Angle Limit to CCW Angle Limit , it is set as 1.
        /// </summary>
        public const byte AngleLimitError = 2;

        /// <summary>
        /// When internal temperature of DYNAMIXEL is out of the range of operating temperature set in the Control table, it is set as 1.
        /// </summary>
        public const byte OverheatingError = 4;

        /// <summary>
        /// When an instruction is out of the range for use, it is set as 1.
        /// </summary>
        public const byte RangeError = 8;

        /// <summary>
        /// When the Checksum of the transmitted Instruction Packet is incorrect, it is set as 1.
        /// </summary>
        public const byte ChecksumError = 16;

        /// <summary>
        /// When the current load cannot be controlled by the set Torque, it is set as 1.
        /// </summary>
        public const byte OverloadError = 32;

        /// <summary>
        /// In case of sending an undefined instruction or delivering the action instruction without the Reg Write instruction, it is set as 1.
        /// </summary>
        public const byte InstructionError = 64;

        /// <summary>
        /// Unused/Undefined error.
        /// </summary>
        public const byte Undefined = 128;
    }
}
