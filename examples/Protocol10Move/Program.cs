﻿
using DynamixelCSharp.Channels;
using DynamixelCSharp.Protocol10;

Random rnd = new Random();

// Example use device id 1
byte deviceId = 1;

// Memory locations to write
MemoryLocation torque = new MemoryLocation(0x18, 0x01, AccessMode.Write);
MemoryLocation goalPosition = new MemoryLocation(0x1E, 0x02, AccessMode.Write);

// Create and open on COM3 with baud rate 1,000,000
// Note: The baud rate must match the baud rate of the Dynamixel device
var channel = new DynamixelSerialChannel("COM3", 1000000);

// Create a client that use protocol 1.0 to communicate
var client = new Protocol10Client(channel);

// Enable torque on the device
client.Write(deviceId, torque, 0x01);

// Set the goal position to 500
for (var i = 0; i < 25; i++)
{
    ushort position = (ushort)rnd.Next(300, 700);

    client.Write(deviceId, goalPosition, BitConverter.GetBytes(position));

    await Task.Delay(200);
}

// Disable torque on the device
client.Write(deviceId, torque, 0x00);

// Close the channel
channel.Close();