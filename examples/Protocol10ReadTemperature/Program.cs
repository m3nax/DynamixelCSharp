using DynamixelCSharp.Channels;
using DynamixelCSharp.Protocol10;
using DynamixelCSharp.Protocol10.Devices;

// Example we will use device with id 1
byte deviceId = 1;

// Example device is a Dynamixel AX-12A
var memoryProfile = new Ax12aMemoryProfile();

// Create and open on COM3 with baud rate 1,000,000
// Note: The baud rate must match the baud rate of the Dynamixel device
var channel = new DynamixelSerialChannel("COM3", 1000000);

// Create a client that use protocol 1.0 to communicate
var client = new Protocol10Client(channel);

// Execute the ping instruction
var result = client.Read(deviceId, memoryProfile.PresentTemperature);

// Display the result
Console.WriteLine($"Temperature: {result[0]} °C");

// Close the channel
channel.Close();