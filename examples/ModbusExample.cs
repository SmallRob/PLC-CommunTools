using System;
using System.Threading.Tasks;
using Commun.Modbus;
using Commun.Protocols;

namespace PLCCommunTools.Examples;

/// <summary>
/// Modbus 协议使用示例
/// </summary>
public class ModbusExample
{
    /// <summary>
    /// 基本使用示例
    /// </summary>
    public static async Task BasicUsage()
    {
        Console.WriteLine("=== Modbus 基本使用示例 ===");

        // 创建 Modbus 协议实例
        var protocol = new ModbusProtocol();

        // 配置连接参数
        var config = new ProtocolConfig
        {
            Address = "192.168.1.100",
            Port = 502,
            Timeout = TimeSpan.FromSeconds(30),
            RetryCount = 3
        };

        try
        {
            // 连接到 Modbus 设备
            Console.WriteLine("正在连接到 Modbus 设备...");
            var connected = await protocol.ConnectAsync(config);

            if (connected)
            {
                Console.WriteLine("连接成功！");

                // 读取保持寄存器
                Console.WriteLine("\n读取保持寄存器...");
                var readRequest = new ProtocolRequest
                {
                    CommandType = "ReadHoldingRegisters",
                    Register = 100,
                    Length = 10
                };

                var readResponse = await protocol.SendAsync(readRequest);

                if (readResponse.Success)
                {
                    Console.WriteLine($"读取成功！数据长度: {readResponse.Data?.Length ?? 0} 字节");
                }
                else
                {
                    Console.WriteLine($"读取失败: {readResponse.ErrorMessage}");
                }

                // 写入单个寄存器
                Console.WriteLine("\n写入单个寄存器...");
                var writeRequest = new ProtocolRequest
                {
                    CommandType = "WriteSingleRegister",
                    Register = 200,
                    Data = BitConverter.GetBytes((ushort)12345)
                };

                var writeResponse = await protocol.SendAsync(writeRequest);

                if (writeResponse.Success)
                {
                    Console.WriteLine("写入成功！");
                }
                else
                {
                    Console.WriteLine($"写入失败: {writeResponse.ErrorMessage}");
                }

                // 读取线圈状态
                Console.WriteLine("\n读取线圈状态...");
                var coilRequest = new ProtocolRequest
                {
                    CommandType = "ReadCoils",
                    Register = 0,
                    Length = 8
                };

                var coilResponse = await protocol.SendAsync(coilRequest);

                if (coilResponse.Success)
                {
                    Console.WriteLine($"读取成功！数据: {BitConverter.ToString(coilResponse.Data ?? Array.Empty<byte>())}");
                }
                else
                {
                    Console.WriteLine($"读取失败: {coilResponse.ErrorMessage}");
                }

                // 断开连接
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
            Console.WriteLine($"发生错误: {ex.Message}");
        }
        finally
        {
            protocol.Dispose();
        }
    }

    /// <summary>
    /// 使用插件管理器的示例
    /// </summary>
    public static async Task PluginUsage()
    {
        Console.WriteLine("\n=== Modbus 插件使用示例 ===");

        // 创建插件管理器
        var pluginManager = new ProtocolPluginManager("plugins");

        // 创建 Modbus 插件
        var plugin = new ModbusPlugin();

        // 获取协议元数据
        var metadata = plugin.GetMetadata();
        Console.WriteLine($"协议名称: {metadata.Name}");
        Console.WriteLine($"版本: {metadata.Version}");
        Console.WriteLine($"描述: {metadata.Description}");
        Console.WriteLine($"支持的命令: {string.Join(", ", metadata.SupportedCommands)}");

        // 创建协议实例
        var protocol = plugin.CreateProtocol();
        Console.WriteLine($"创建的协议实例: {protocol.Name} v{protocol.Version}");

        // 使用协议
        var config = new ProtocolConfig
        {
            Address = "192.168.1.100",
            Port = 502
        };

        try
        {
            var connected = await protocol.ConnectAsync(config);
            if (connected)
            {
                Console.WriteLine("连接成功！");
                await protocol.DisconnectAsync();
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

    /// <summary>
    /// 批量读取示例
    /// </summary>
    public static async Task BatchReadExample()
    {
        Console.WriteLine("\n=== Modbus 批量读取示例 ===");

        var protocol = new ModbusProtocol();
        var config = new ProtocolConfig
        {
            Address = "192.168.1.100",
            Port = 502
        };

        try
        {
            var connected = await protocol.ConnectAsync(config);
            if (!connected)
            {
                Console.WriteLine("连接失败！");
                return;
            }

            // 批量读取多个寄存器区域
            var registers = new[] { 100, 200, 300, 400, 500 };

            foreach (var register in registers)
            {
                var request = new ProtocolRequest
                {
                    CommandType = "ReadHoldingRegisters",
                    Register = register,
                    Length = 10
                };

                var response = await protocol.SendAsync(request);

                if (response.Success)
                {
                    Console.WriteLine($"寄存器 {register}: 读取成功 ({response.Data?.Length ?? 0} 字节)");
                }
                else
                {
                    Console.WriteLine($"寄存器 {register}: 读取失败 - {response.ErrorMessage}");
                }
            }

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

    /// <summary>
    /// 错误处理示例
    /// </summary>
    public static async Task ErrorHandlingExample()
    {
        Console.WriteLine("\n=== Modbus 错误处理示例 ===");

        var protocol = new ModbusProtocol();

        // 订阅错误事件
        protocol.ErrorOccurred += (sender, args) =>
        {
            Console.WriteLine($"错误事件: {args.Message}");
        };

        // 尝试连接到不存在的设备
        var config = new ProtocolConfig
        {
            Address = "192.168.1.999", // 不存在的地址
            Port = 502,
            Timeout = TimeSpan.FromSeconds(5)
        };

        try
        {
            var connected = await protocol.ConnectAsync(config);

            if (!connected)
            {
                Console.WriteLine("连接失败（预期行为）");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"捕获异常: {ex.Message}");
        }
        finally
        {
            protocol.Dispose();
        }
    }
}
