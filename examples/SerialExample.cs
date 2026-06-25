using System;
using System.Threading.Tasks;
using Commun.Serial;
using Commun.Protocols;

namespace PLCCommunTools.Examples;

/// <summary>
/// 串口通讯协议使用示例
/// </summary>
public class SerialExample
{
    public static async Task BasicUsage()
    {
        Console.WriteLine("=== 串口通讯基本使用示例 ===");

        var protocol = new SerialProtocol();

        var config = new ProtocolConfig
        {
            Address = "COM1",
            Port = 9600,
            Timeout = TimeSpan.FromSeconds(3),
            Parameters = new Dictionary<string, object>
            {
                { "DataBits", 8 },
                { "Parity", "None" },
                { "StopBits", "One" }
            }
        };

        try
        {
            Console.WriteLine($"可用串口: {string.Join(", ", SerialProtocol.GetAvailablePorts())}");
            Console.WriteLine("正在打开串口...");

            var connected = await protocol.ConnectAsync(config);

            if (connected)
            {
                Console.WriteLine("串口已打开！");

                var sendRequest = new ProtocolRequest
                {
                    Data = new byte[] { 0x01, 0x03, 0x00, 0x64, 0x00, 0x0A, 0xC5, 0xD4 }
                };
                var sendResponse = await protocol.SendAsync(sendRequest);
                Console.WriteLine($"发送: {(sendResponse.Success ? "成功" : "失败")}");

                await Task.Delay(500);

                await protocol.DisconnectAsync();
                Console.WriteLine("串口已关闭");
            }
            else
            {
                Console.WriteLine("打开串口失败！");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
        }
        finally
        {
            protocol.Dispose();
        }
    }

    public static async Task PluginUsage()
    {
        Console.WriteLine("\n=== 串口插件使用示例 ===");

        var plugin = new SerialPlugin();
        var metadata = plugin.GetMetadata();
        Console.WriteLine($"协议: {metadata.Name} v{metadata.Version}");
        Console.WriteLine($"描述: {metadata.Description}");
        Console.WriteLine($"命令: {string.Join(", ", metadata.SupportedCommands)}");
    }
}
