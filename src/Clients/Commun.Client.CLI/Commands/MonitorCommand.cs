using System.CommandLine;
using System.Net.Http.Json;

namespace Commun.Client.CLI.Commands;

public class MonitorCommand : Command
{
    public MonitorCommand() : base("monitor", "Monitor device register changes")
    {
        var deviceOption = new Option<string>("--device") { Required = true, Description = "Device ID" };
        var registerOption = new Option<int>("--register") { Required = true, Description = "Register address" };
        var intervalOption = new Option<int>("--interval") { Description = "Polling interval in milliseconds" };
        intervalOption.DefaultValueFactory = _ => 1000;

        Add(deviceOption);
        Add(registerOption);
        Add(intervalOption);

        SetAction((ParseResult result) =>
        {
            var deviceId = result.GetValue(deviceOption)!;
            var register = result.GetValue(registerOption);
            var interval = result.GetValue(intervalOption);

            var client = new HttpClient();
            var baseUrl = "http://localhost:5000";

            Console.WriteLine($"Monitoring device {deviceId} register {register}...");
            Console.WriteLine($"Polling interval: {interval}ms");
            Console.WriteLine("Press Ctrl+C to stop");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var request = new
                    {
                        CommandType = "ReadHoldingRegisters",
                        Register = register,
                        Length = 1
                    };

                    var response = client.PostAsJsonAsync($"{baseUrl}/api/protocol/{deviceId}/send", request).GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        var respResult = response.Content.ReadFromJsonAsync<ProtocolResponse>().GetAwaiter().GetResult();

                        if (respResult?.Success == true && respResult.Data?.Length >= 2)
                        {
                            var val = BitConverter.ToUInt16(respResult.Data, 0);
                            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Register {register} = {val}");
                        }
                    }

                    Task.Delay(interval, cts.Token).GetAwaiter().GetResult();
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nMonitoring stopped");
            }
        });
    }
}
