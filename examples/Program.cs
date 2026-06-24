using System;
using System.Threading.Tasks;
using PLCCommunTools.Examples;

namespace PLCCommunTools;

/// <summary>
/// PLC-CommunTools 使用示例主程序
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("PLC-CommunTools 使用示例");
        Console.WriteLine("=====================");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("请选择示例:");
            Console.WriteLine("1. Modbus 协议示例");
            Console.WriteLine("2. MQTT 协议示例");
            Console.WriteLine("3. 串口通讯协议示例");
            Console.WriteLine("4. 综合示例（多协议监控）");
            Console.WriteLine("5. 协议转换示例");
            Console.WriteLine("6. 设备发现示例");
            Console.WriteLine("0. 退出");
            Console.WriteLine();
            Console.Write("请输入选择 (0-6): ");

            var choice = Console.ReadLine();

            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await RunModbusExamples();
                        break;
                    case "2":
                        await RunMqttExamples();
                        break;
                    case "3":
                        await RunSerialExamples();
                        break;
                    case "4":
                        await ComprehensiveExample.MultiProtocolMonitoring();
                        break;
                    case "5":
                        await ComprehensiveExample.ProtocolBridgeExample();
                        break;
                    case "6":
                        await ComprehensiveExample.DeviceDiscoveryExample();
                        break;
                    case "0":
                        Console.WriteLine("感谢使用 PLC-CommunTools！");
                        return;
                    default:
                        Console.WriteLine("无效选择，请重新输入");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运行示例时发生错误: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("按任意键继续...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static async Task RunModbusExamples()
    {
        Console.WriteLine("=== Modbus 协议示例 ===");
        Console.WriteLine();

        Console.WriteLine("1. 基本使用示例");
        await ModbusExample.BasicUsage();

        Console.WriteLine("\n2. 插件使用示例");
        await ModbusExample.PluginUsage();

        Console.WriteLine("\n3. 批量读取示例");
        await ModbusExample.BatchReadExample();

        Console.WriteLine("\n4. 错误处理示例");
        await ModbusExample.ErrorHandlingExample();
    }

    private static async Task RunMqttExamples()
    {
        Console.WriteLine("=== MQTT 协议示例 ===");
        Console.WriteLine();

        Console.WriteLine("1. 基本使用示例");
        await MqttExample.BasicUsage();

        Console.WriteLine("\n2. 插件使用示例");
        await MqttExample.PluginUsage();

        Console.WriteLine("\n3. 发布/订阅模式示例");
        await MqttExample.PubSubExample();

        Console.WriteLine("\n4. 错误处理示例");
        await MqttExample.ErrorHandlingExample();
    }

    private static async Task RunSerialExamples()
    {
        Console.WriteLine("=== 串口通讯协议示例 ===");
        Console.WriteLine();

        Console.WriteLine("1. 基本使用示例");
        await SerialExample.BasicUsage();

        Console.WriteLine("\n2. 插件使用示例");
        await SerialExample.PluginUsage();
    }
}
