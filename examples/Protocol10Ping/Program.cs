
using DynamixelCSharp.Channels;
using DynamixelCSharp.Protocol10;

// Example we will use device with id 1
byte deviceId = 1;

// Create and open on COM3 with baud rate 1,000,000
// Note: The baud rate must match the baud rate of the Dynamixel device
var channel = new DynamixelSerialChannel("COM3", 1000000);

// Create a client that use protocol 1.0 to communicate
var client = new Protocol10Client(channel);

// Execute the ping instruction
client.Ping(deviceId);

// Close the channel
channel.Close();