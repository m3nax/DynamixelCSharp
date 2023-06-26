
using DynamixelCSharp.Channels;
using System.Globalization;

// Create and open on COM3 with baud rate 1,000,000
// Note: The baud rate must match the baud rate of the Dynamixel device
var channel = new DynamixelSerialChannel("COM3", 1000000);

// Protocol 1.0 ping command for device with id 1
// Header       = 0xFF, 0xFF
// Device ID    = 0x01
// Length       = 0x02
// Instruction  = 0x01
// Checksum     = 0xFB
var pingCommand = new byte[] { 0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFB };

// Send command and wait for response
// For protocol 1.0 the ping command response length is 6 bytes
// Note: Channel will automatically open if not already open.
var response = channel.Send(pingCommand, 6);

// Expected response is 0xFF, 0xFF, 0x01, 0x02, 0x00, 0xFC
foreach (var packet in response)
{
    Console.Write(packet.ToString("X2", CultureInfo.InvariantCulture) + " ");
}

// Close the channel
channel.Close();