
using DynamixelCSharp.Channels;

var channel = new DynamixelSerialChannel("COM3", 1000000);

channel.Open();

// Protocol 1.0 ping command for device with id 1
var pingCommand = new byte[] { 0xFF, 0xFF, 0x01, 0x02, 0x01, 0xFB };

// Send command and wait for response
// For protocol 1.0 the ping command response length is 6 bytes
var response = channel.Send(pingCommand, 6);

// Expected response is 0xFF, 0xFF, 0x01, 0x02, 0x00, 0xFC
foreach (var packet in response)
{
    Console.Write(packet.ToString("X2") + " ");
}