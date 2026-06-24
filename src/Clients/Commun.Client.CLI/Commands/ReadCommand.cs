using System.CommandLine;
using System.Net.Http.Json;

namespace Commun.Client.CLI.Commands;

public class ReadCommand : Command
{
    public ReadCommand() : base("read", "Read register value from device")
    {
        var deviceOption = new Option<string>("--device") { Required = true, Description = "Device ID" };
        var registerOption = new Option<int>("--register") { Required = true, Description = "Register address" };
        var lengthOption = new Option<int>("--length") { Description = "Number of registers to read" };
        lengthOption.DefaultValueFactory = _ => 1;

        Add(deviceOption);
        Add(registerOption);
        Add(lengthOption);

        SetAction((ParseResult result) =>
        {
            var deviceId = result.GetValue(deviceOption)!;
            var register = result.GetValue(registerOption);
            var length = result.GetValue(lengthOption);

            var client = new HttpClient();
            var baseUrl = "http://localhost:5000";

            var request = new
            {
                CommandType = "ReadHoldingRegisters",
                Register = register,
                Length = length
            };

            var response = client.PostAsJsonAsync($"{baseUrl}/api/protocol/{deviceId}/send", request).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            var respResult = response.Content.ReadFromJsonAsync<ProtocolResponse>().GetAwaiter().GetResult();

            if (respResult?.Success == true)
            {
                Console.WriteLine($"Read successful:");
                Console.WriteLine($"Data: {BitConverter.ToString(respResult.Data ?? Array.Empty<byte>())}");
            }
            else
            {
                Console.WriteLine($"Read failed: {respResult?.ErrorMessage}");
            }
        });
    }
}

public class ProtocolResponse
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
