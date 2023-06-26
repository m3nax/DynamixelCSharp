namespace DynamixelCSharp.Protocol10.Devices
{
    /// <summary>
    /// Memory profile of the AX-12A device.
    /// Doc: https://emanual.robotis.com/docs/en/dxl/ax/ax-12a/#control-table-of-eeprom-area.
    /// </summary>
    public struct Ax12aMemoryProfile : IMemoryProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ax12aMemoryProfile"/> struct.
        /// </summary>
        public Ax12aMemoryProfile()
        {
        }

        /// <inheritdoc/>
        public MemoryLocation ModelNumber
            => new MemoryLocation(0, 2, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation FirmwareVersion
            => new MemoryLocation(2, 1, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation ID
            => new MemoryLocation(3, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation BaudRate
            => new MemoryLocation(4, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation ReturnDelayTime
            => new MemoryLocation(5, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation CWAngleLimit
            => new MemoryLocation(6, 2, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation CCWAngleLimit
            => new MemoryLocation(8, 2, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation TemperatureLimit
            => new MemoryLocation(11, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation MinVoltageLimit
            => new MemoryLocation(12, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation MaxVoltageLimit
            => new MemoryLocation(13, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation MaxTorque
            => new MemoryLocation(14, 2, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation StatusReturnLevel
            => new MemoryLocation(16, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation AlarmLED
            => new MemoryLocation(17, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation Shutdown
            => new MemoryLocation(18, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation TorqueEnable
            => new MemoryLocation(24, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation LED
            => new MemoryLocation(25, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation CWComplianceMargin
            => new MemoryLocation(26, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation CCWComplianceMargin
            => new MemoryLocation(27, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation CWComplianceSlope
            => new MemoryLocation(28, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation CCWComplianceSlope
            => new MemoryLocation(29, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation GoalPosition
            => new MemoryLocation(30, 2, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation MovingSpeed
            => new MemoryLocation(32, 2, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation TorqueLimit
            => new MemoryLocation(34, 2, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation PresentPosition
            => new MemoryLocation(36, 2, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation PresentSpeed
            => new MemoryLocation(38, 2, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation PresentLoad
            => new MemoryLocation(40, 2, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation PresentVoltage
            => new MemoryLocation(42, 1, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation PresentTemperature
            => new MemoryLocation(43, 1, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation Registered
            => new MemoryLocation(44, 1, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation Moving
            => new MemoryLocation(46, 1, AccessMode.Read);

        /// <inheritdoc/>
        public MemoryLocation Lock
            => new MemoryLocation(47, 1, AccessMode.ReadWrite);

        /// <inheritdoc/>
        public MemoryLocation Punch
            => new MemoryLocation(48, 2, AccessMode.ReadWrite);
    }
}
