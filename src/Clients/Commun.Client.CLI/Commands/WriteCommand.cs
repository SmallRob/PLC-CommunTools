using System.CommandLine;
using System.Net.Http.Json;

namespace Commun.Client.CLI.Commands;

public class WriteCommand : Command
{
    public WriteCommand() : base("write", "Write value to device register")
    {
        var deviceOption = new Option<string>("--device") { Required = true, Description = "Device ID" };
        var registerOption = new Option<int>("--register") { Required = true, Description = "Register address" };
        var valueOption = new Option<int>("--value") { Required = true, Description = "Value to write" };

        Add(deviceOption);
        Add(registerOption);
        Add(valueOption);

        SetAction((ParseResult result) =>
        {
            var deviceId = result.GetValue(deviceOption)!;
            var register = result.GetValue(registerOption);
            var value = result.GetValue(valueOption);

            var client = new HttpClient();
            var baseUrl = "http://localhost:5000";

            var request = new
            {
                CommandType = "WriteSingleRegister",
                Register = register,
                Data = BitConverter.GetBytes((ushort)value)
            };

            var response = client.PostAsJsonAsync($"{baseUrl}/api/protocol/{deviceId}/send", request).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            var respResult = response.Content.ReadFromJsonAsync<ProtocolResponse>().GetAwaiter().GetResult();

            if (respResult?.Success == true)
            {
                Console.WriteLine($"Write successful");
            }
            else
            {
                Console.WriteLine($"Write failed: {respResult?.ErrorMessage}");
            }
        });
    }
}
