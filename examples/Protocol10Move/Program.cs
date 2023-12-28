using DynamixelCSharp.Channels;
using DynamixelCSharp.Protocol10;
using DynamixelCSharp.Protocol10.Devices;
using System.Security.Cryptography;

Random rnd = new Random();

// Example we will use device with id 1
byte deviceId = 1;

// Example device is a Dynamixel AX-12A
var memoryProfile = new Ax12aMemoryProfile();

// Create and open the communication channel on COM3 with baud rate 1,000,000
// Note: The baud rate must match the baud rate configured in the Dynamixel device
var channel = new DynamixelSerialChannel("COM3", 1000000);

// Create a client that use protocol 1.0 to communicate
var client = new Protocol10Client(channel);

// Enable torque on the device
client.Write(deviceId, memoryProfile.TorqueEnable, 0x01);

// Set the goal position to random position between 300 and 700
// Execute the operation immediately (no need to call Action)
for (var i = 0; i < 15; i++)
{
    ushort position = (ushort)RandomNumberGenerator.GetInt32(300, 700);

    client.Write(deviceId, memoryProfile.GoalPosition, BitConverter.GetBytes(position));

    await Task.Delay(500).ConfigureAwait(true);
}

// Set the goal position to random position between 300 and 700
// The operation are registered in the device and executed when 'Action' method is called
for (var i = 0; i < 15; i++)
{
    ushort position = (ushort)RandomNumberGenerator.GetInt32(300, 700);

    client.RegWrite(deviceId, memoryProfile.GoalPosition, BitConverter.GetBytes(position));
    client.Action();

    await Task.Delay(500).ConfigureAwait(true);
}

// Disable torque on the device
client.Write(deviceId, memoryProfile.TorqueEnable, 0x00);

// Close the channel
channel.Close();