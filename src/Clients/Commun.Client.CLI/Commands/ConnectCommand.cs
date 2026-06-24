using System.CommandLine;
using Commun.Client.CLI.Services;
using Commun.Server.API.Models;

namespace Commun.Client.CLI.Commands;

public class ConnectCommand : Command
{
    public ConnectCommand() : base("connect", "Connect to a device")
    {
        var protocolOption = new Option<string>("--protocol") { Required = true, Description = "Protocol name (Modbus, MQTT)" };
        var addressOption = new Option<string>("--address") { Required = true, Description = "Device address" };
        var portOption = new Option<int>("--port") { Required = true, Description = "Device port" };
        var nameOption = new Option<string>("--name") { Description = "Device name" };

        Add(protocolOption);
        Add(addressOption);
        Add(portOption);
        Add(nameOption);

        SetAction((ParseResult result) =>
        {
            var protocol = result.GetValue(protocolOption)!;
            var address = result.GetValue(addressOption)!;
            var port = result.GetValue(portOption);
            var name = result.GetValue(nameOption);

            var service = new DeviceService("http://localhost:5000");

            var dto = new CreateDeviceDto
            {
                Name = name ?? $"{protocol} Device",
                Protocol = protocol,
                Address = address,
                Port = port
            };

            var device = service.CreateDeviceAsync(dto).GetAwaiter().GetResult();
            Console.WriteLine($"Device created: {device.Id}");
            Console.WriteLine($"Name: {device.Name}");
            Console.WriteLine($"Protocol: {device.Protocol}");
            Console.WriteLine($"Address: {device.Address}:{device.Port}");
        });
    }
}
