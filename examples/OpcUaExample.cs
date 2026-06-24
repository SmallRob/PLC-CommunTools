using System;
using System.Threading.Tasks;
using Commun.OPCUA;
using Commun.Protocols;

namespace PLCCommunTools.Examples;

/// <summary>
/// OPC UA 协议使用示例
/// </summary>
public class OpcUaExample
{
    public static async Task BasicUsage()
    {
        Console.WriteLine("=== OPC UA 基本使用示例 ===");

        var protocol = new OpcUaProtocol();

        var config = new ProtocolConfig
        {
            Address = "localhost",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(30)
        };

        try
        {
            Console.WriteLine("正在连接到 OPC UA 服务器...");
            var connected = await protocol.ConnectAsync(config);

            if (connected)
            {
                Console.WriteLine("连接成功！");

                Console.WriteLine("\n读取节点值...");
                var readRequest = new ProtocolRequest
                {
                    CommandType = "Read",
                    Register = 1001
                };
                var readResponse = await protocol.SendAsync(readRequest);
                Console.WriteLine($"读取: {(readResponse.Success ? "成功" : $"失败 - {readResponse.ErrorMessage}")}");

                await protocol.DisconnectAsync();
                Console.WriteLine("\n已断开连接");
            }
            else
            {
                Console.WriteLine("连接失败！");
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
        Console.WriteLine("\n=== OPC UA 插件使用示例 ===");

        var plugin = new OpcUaPlugin();
        var metadata = plugin.GetMetadata();
        Console.WriteLine($"协议: {metadata.Name} v{metadata.Version}");
        Console.WriteLine($"描述: {metadata.Description}");
        Console.WriteLine($"命令: {string.Join(", ", metadata.SupportedCommands)}");
        Console.WriteLine($"默认配置:");
        foreach (var kv in metadata.DefaultConfig)
        {
            Console.WriteLine($"  {kv.Key} = {kv.Value}");
        }
    }

    public static async Task ReadWriteExample()
    {
        Console.WriteLine("\n=== OPC UA 读写示例 ===");

        var protocol = new OpcUaProtocol();
        var config = new ProtocolConfig
        {
            Address = "localhost",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(30)
        };

        try
        {
            var connected = await protocol.ConnectAsync(config);
            if (!connected)
            {
                Console.WriteLine("连接失败！");
                return;
            }

            Console.WriteLine("读取节点...");
            var readResult = await protocol.ReadNodeAsync("ns=2;s=Demo.Static.Scalar.Int32");
            Console.WriteLine($"读取: {(readResult.Success ? "成功" : "失败")}");

            Console.WriteLine("\n写入节点...");
            var writeResult = await protocol.WriteNodeAsync("ns=2;s=Demo.Static.Scalar.Int32", 12345);
            Console.WriteLine($"写入: {(writeResult.Success ? "成功" : $"失败 - {writeResult.ErrorMessage}")}");

            await protocol.DisconnectAsync();
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

    public static async Task SubscriptionExample()
    {
        Console.WriteLine("\n=== OPC UA 订阅监控示例 ===");

        var protocol = new OpcUaProtocol();
        var config = new ProtocolConfig
        {
            Address = "localhost",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(30)
        };

        try
        {
            var connected = await protocol.ConnectAsync(config);
            if (!connected)
            {
                Console.WriteLine("连接失败！");
                return;
            }

            Console.WriteLine("已连接到 OPC UA 服务器");

            var nodesToMonitor = new[]
            {
                "ns=2;s=Demo.Static.Scalar.Int32",
                "ns=2;s=Demo.Static.Scalar.Double",
                "ns=2;s=Demo.Static.Scalar.Boolean"
            };

            protocol.NodeValueChanged += (sender, args) =>
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] 节点变化: {args.NodeId} = {args.Value} (状态: {args.StatusCode})");
            };

            Console.WriteLine("\n创建订阅...");
            foreach (var nodeId in nodesToMonitor)
            {
                var result = protocol.Subscribe(nodeId, publishingInterval: 500);
                Console.WriteLine($"  {nodeId}: {(result ? "订阅成功" : "订阅失败")}");
            }

            Console.WriteLine($"\n活跃订阅数: {protocol.SubscriptionCount}");
            Console.WriteLine("等待数据变化中 (10秒)...");
            Console.WriteLine("按 Ctrl+C 提前停止");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                await Task.Delay(10000, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\n监控已停止");
            }

            Console.WriteLine("\n取消订阅...");
            foreach (var nodeId in nodesToMonitor)
            {
                protocol.Unsubscribe(nodeId);
            }

            Console.WriteLine($"剩余订阅数: {protocol.SubscriptionCount}");

            await protocol.DisconnectAsync();
            Console.WriteLine("已断开连接");
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
}
