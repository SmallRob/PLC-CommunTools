using System;
using System.Threading.Tasks;
using Commun.Modbus;
using Commun.MQTT;
using Commun.Serial;
using Commun.Protocols;

namespace PLCCommunTools.Examples;

/// <summary>
/// 综合使用示例，展示如何使用多个协议
/// </summary>
public class ComprehensiveExample
{
    /// <summary>
    /// 多协议设备监控示例
    /// </summary>
    public static async Task MultiProtocolMonitoring()
    {
        Console.WriteLine("=== 多协议设备监控示例 ===");

        // 创建协议管理器
        var protocols = new Dictionary<string, IProtocol>();

        try
        {
            // 初始化 Modbus 协议
            var modbusProtocol = new ModbusProtocol();
            var modbusConfig = new ProtocolConfig
            {
                Address = "192.168.1.100",
                Port = 502,
                Timeout = TimeSpan.FromSeconds(30)
            };

            // 初始化 MQTT 协议
            var mqttProtocol = new MqttProtocol();
            var mqttConfig = new ProtocolConfig
            {
                Address = "localhost",
                Port = 1883,
                Timeout = TimeSpan.FromSeconds(30)
            };

            // 初始化串口协议
            var serialProtocol = new SerialProtocol();
            var serialConfig = new ProtocolConfig
            {
                Address = "COM1",
                Port = 9600,
                Timeout = TimeSpan.FromSeconds(3)
            };

            // 连接所有协议
            Console.WriteLine("正在连接到设备...");

            var modbusConnected = await modbusProtocol.ConnectAsync(modbusConfig);
            Console.WriteLine($"Modbus: {(modbusConnected ? "连接成功" : "连接失败")}");

            var mqttConnected = await mqttProtocol.ConnectAsync(mqttConfig);
            Console.WriteLine($"MQTT: {(mqttConnected ? "连接成功" : "连接失败")}");

            var serialConnected = await serialProtocol.ConnectAsync(serialConfig);
            Console.WriteLine($"Serial: {(serialConnected ? "连接成功" : "连接失败")}");

            // 监控循环
            Console.WriteLine("\n开始监控设备...");
            Console.WriteLine("按 Ctrl+C 停止监控");

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
                    // 从 Modbus 读取数据
                    if (modbusConnected)
                    {
                        var modbusRequest = new ProtocolRequest
                        {
                            CommandType = "ReadHoldingRegisters",
                            Register = 100,
                            Length = 10
                        };

                        var modbusResponse = await modbusProtocol.SendAsync(modbusRequest);

                        if (modbusResponse.Success)
                        {
                            Console.WriteLine($"[Modbus] 数据读取成功: {modbusResponse.Data?.Length ?? 0} 字节");
                        }
                    }

                    // 通过 MQTT 发布状态
                    if (mqttConnected)
                    {
                        var mqttRequest = new ProtocolRequest
                        {
                            CommandType = "Publish",
                            Data = System.Text.Encoding.UTF8.GetBytes($"Status update at {DateTime.Now}"),
                            Parameters = new Dictionary<string, object>
                            {
                                { "topic", "device/status" }
                            }
                        };

                        await mqttProtocol.SendAsync(mqttRequest);
                    }

                    // 通过串口发送数据
                    if (serialConnected)
                    {
                        var serialRequest = new ProtocolRequest
                        {
                            Data = System.Text.Encoding.UTF8.GetBytes($"Ping at {DateTime.Now}")
                        };

                        var serialResponse = await serialProtocol.SendAsync(serialRequest);

                        if (serialResponse.Success)
                        {
                            Console.WriteLine($"[Serial] 数据发送成功");
                        }
                    }

                    await Task.Delay(5000, cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\n监控已停止");
            }

            // 断开所有连接
            Console.WriteLine("\n正在断开连接...");

            if (modbusConnected)
            {
                await modbusProtocol.DisconnectAsync();
                Console.WriteLine("Modbus: 已断开");
            }

            if (mqttConnected)
            {
                await mqttProtocol.DisconnectAsync();
                Console.WriteLine("MQTT: 已断开");
            }

            if (serialConnected)
            {
                await serialProtocol.DisconnectAsync();
                Console.WriteLine("Serial: 已断开");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发生错误: {ex.Message}");
        }
        finally
        {
            // 释放所有协议资源
            foreach (var protocol in protocols.Values)
            {
                protocol.Dispose();
            }
        }
    }

    /// <summary>
    /// 协议转换示例
    /// </summary>
    public static async Task ProtocolBridgeExample()
    {
        Console.WriteLine("\n=== 协议转换示例 ===");

        // 创建 Modbus 到 MQTT 的桥接
        var modbusProtocol = new ModbusProtocol();
        var mqttProtocol = new MqttProtocol();

        try
        {
            // 连接 Modbus 设备
            var modbusConfig = new ProtocolConfig
            {
                Address = "192.168.1.100",
                Port = 502
            };

            var modbusConnected = await modbusProtocol.ConnectAsync(modbusConfig);

            if (!modbusConnected)
            {
                Console.WriteLine("Modbus 连接失败");
                return;
            }

            // 连接 MQTT 代理
            var mqttConfig = new ProtocolConfig
            {
                Address = "localhost",
                Port = 1883
            };

            var mqttConnected = await mqttProtocol.ConnectAsync(mqttConfig);

            if (!mqttConnected)
            {
                Console.WriteLine("MQTT 连接失败");
                return;
            }

            Console.WriteLine("协议桥接已建立");

            // 桥接数据
            for (int i = 0; i < 5; i++)
            {
                // 从 Modbus 读取数据
                var modbusRequest = new ProtocolRequest
                {
                    CommandType = "ReadHoldingRegisters",
                    Register = 100,
                    Length = 1
                };

                var modbusResponse = await modbusProtocol.SendAsync(modbusRequest);

                if (modbusResponse.Success && modbusResponse.Data != null)
                {
                    // 转换数据
                    var value = BitConverter.ToInt16(modbusResponse.Data, 0);
                    var message = $"{{\"register\": 100, \"value\": {value}, \"timestamp\": \"{DateTime.Now:O}\"}}";

                    // 发布到 MQTT
                    var mqttRequest = new ProtocolRequest
                    {
                        CommandType = "Publish",
                        Data = System.Text.Encoding.UTF8.GetBytes(message),
                        Parameters = new Dictionary<string, object>
                        {
                            { "topic", "modbus/data" }
                        }
                    };

                    await mqttProtocol.SendAsync(mqttRequest);
                    Console.WriteLine($"桥接数据: Modbus[100] = {value} -> MQTT[modbus/data]");
                }

                await Task.Delay(2000);
            }

            // 断开连接
            await modbusProtocol.DisconnectAsync();
            await mqttProtocol.DisconnectAsync();

            Console.WriteLine("协议桥接已关闭");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
        }
        finally
        {
            modbusProtocol.Dispose();
            mqttProtocol.Dispose();
        }
    }

    /// <summary>
    /// 设备发现示例
    /// </summary>
    public static async Task DeviceDiscoveryExample()
    {
        Console.WriteLine("\n=== 设备发现示例 ===");

        // 创建协议插件管理器
        var pluginManager = new ProtocolPluginManager("plugins");

        // 获取所有可用协议
        var protocols = pluginManager.GetAvailableProtocols();

        Console.WriteLine("可用协议:");
        foreach (var protocol in protocols)
        {
            Console.WriteLine($"  - {protocol.Name} v{protocol.Version}: {protocol.Description}");
            Console.WriteLine($"    支持的命令: {string.Join(", ", protocol.SupportedCommands)}");
            Console.WriteLine($"    默认配置: {string.Join(", ", protocol.DefaultConfig.Select(kv => $"{kv.Key}={kv.Value}"))}");
        }

        // 尝试连接到设备
        Console.WriteLine("\n尝试连接到设备...");

        foreach (var protocolMetadata in protocols)
        {
            try
            {
                var protocol = pluginManager.CreateProtocol(protocolMetadata.Name);

                var config = new ProtocolConfig
                {
                    Address = "192.168.1.100",
                    Port = Convert.ToInt32(protocolMetadata.DefaultConfig.GetValueOrDefault("Port", 502))
                };

                var connected = await protocol.ConnectAsync(config);

                Console.WriteLine($"{protocolMetadata.Name}: {(connected ? "连接成功" : "连接失败")}");

                if (connected)
                {
                    await protocol.DisconnectAsync();
                }

                protocol.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{protocolMetadata.Name}: 错误 - {ex.Message}");
            }
        }
    }
}
