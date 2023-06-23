namespace DynamixelCSharp.Protocol10.Devices
{
    /// <summary>
    /// Interface that represents the structure of memory of a device.
    /// </summary>
    public interface IMemoryProfile
    {
        /// <summary>
        /// Model number of the device.
        /// </summary>
        public MemoryLocation ModelNumber { get; }

        /// <summary>
        /// Firmware version of the device.
        /// </summary>
        public MemoryLocation FirmwareVersion { get; }

        /// <summary>
        /// Id of the device.
        /// </summary>
        public MemoryLocation ID { get; }

        /// <summary>
        /// Baud rate used by the device for communication.
        /// </summary>
        public MemoryLocation BaudRate { get; }

        /// <summary>
        /// Response Delay Time the device wait before sending the response packet.
        /// </summary>
        public MemoryLocation ReturnDelayTime { get; }

        /// <summary>
        /// Angle limit of device the motion.
        /// </summary>
        public MemoryLocation CWAngleLimit { get; }

        /// <summary>
        /// Angle limit of device the motion.
        /// </summary>
        public MemoryLocation CCWAngleLimit { get; }

        /// <summary>
        /// Temperature limit before the device shutdown.
        /// </summary>
        public MemoryLocation TemperatureLimit { get; }

        /// <summary>
        /// Minimum voltage limit before the device shutdown.
        /// </summary>
        public MemoryLocation MinVoltageLimit { get; }

        /// <summary>
        /// Maximum voltage limit before the device shutdown.
        /// </summary>
        public MemoryLocation MaxVoltageLimit { get; }

        /// <summary>
        /// Max torque that the device can apply.
        /// </summary>
        public MemoryLocation MaxTorque { get; }

        /// <summary>
        /// The Status Return Level (16) decides how to return Status Packet when DYNAMIXEL receives 
        /// an Instruction Packet.
        /// </summary>
        public MemoryLocation StatusReturnLevel { get; }

        /// <summary>
        /// LED for Alarm.
        /// </summary>
        public MemoryLocation AlarmLED { get; }

        /// <summary>
        /// Shutdown Error Information.
        /// </summary>
        public MemoryLocation Shutdown { get; }

        /// <summary>
        /// Motor Torque On/Off.
        /// </summary>
        public MemoryLocation TorqueEnable { get; }

        /// <summary>
        /// Status LED On/Off.
        /// </summary>
        public MemoryLocation LED { get; }

        /// <summary>
        /// CW Compliance Margin.
        /// </summary>
        public MemoryLocation CWComplianceMargin { get; }

        /// <summary>
        /// CCW Compliance Margin.
        /// </summary>
        public MemoryLocation CCWComplianceMargin { get; }

        /// <summary>
        /// CW Compliance Slope.
        /// </summary>
        public MemoryLocation CWComplianceSlope { get; }

        /// <summary>
        /// CCW Compliance Slope.
        /// </summary>
        public MemoryLocation CCWComplianceSlope { get; }

        /// <summary>
        /// Target Position.
        /// </summary>
        public MemoryLocation GoalPosition { get; }

        /// <summary>
        /// Moving Speed.
        /// </summary>
        public MemoryLocation MovingSpeed { get; }

        /// <summary>
        /// Torque Limit.
        /// </summary>
        public MemoryLocation TorqueLimit { get; }

        /// <summary>
        /// Present Position.
        /// </summary>
        public MemoryLocation PresentPosition { get; }

        /// <summary>
        /// Present Speed.
        /// </summary>
        public MemoryLocation PresentSpeed { get; }

        /// <summary>
        /// Present Load.
        /// </summary>
        public MemoryLocation PresentLoad { get; }

        /// <summary>
        /// Present Voltage.
        /// </summary>
        public MemoryLocation PresentVoltage { get; }

        /// <summary>
        /// Present Voltage.
        /// </summary>
        public MemoryLocation PresentTemperature { get; }

        /// <summary>
        /// Indicate if an instruction is registered.
        /// </summary>
        public MemoryLocation Registered { get; }

        /// <summary>
        /// Indicate if the device is moving.
        /// </summary>
        public MemoryLocation Moving { get; }

        /// <summary>
        /// Indicate if EEPROM of the device is locked.
        /// </summary>
        public MemoryLocation Lock { get; }

        /// <summary>
        ///	Minimum Current Threshold.
        /// </summary>
        public MemoryLocation Punch { get; }
    }
}
