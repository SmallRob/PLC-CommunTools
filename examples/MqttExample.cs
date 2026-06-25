using System;
using System.Text;
using System.Threading.Tasks;
using Commun.MQTT;
using Commun.Protocols;

namespace PLCCommunTools.Examples;

/// <summary>
/// MQTT 协议使用示例
/// </summary>
public class MqttExample
{
    /// <summary>
    /// 基本使用示例
    /// </summary>
    public static async Task BasicUsage()
    {
        Console.WriteLine("=== MQTT 基本使用示例 ===");

        // 创建 MQTT 协议实例
        var protocol = new MqttProtocol();

        // 配置连接参数
        var config = new ProtocolConfig
        {
            Address = "localhost",
            Port = 1883,
            Timeout = TimeSpan.FromSeconds(30)
        };

        try
        {
            // 连接到 MQTT 代理
            Console.WriteLine("正在连接到 MQTT 代理...");
            var connected = await protocol.ConnectAsync(config);

            if (connected)
            {
                Console.WriteLine("连接成功！");

                // 发布消息
                Console.WriteLine("\n发布消息...");
                var publishRequest = new ProtocolRequest
                {
                    CommandType = "Publish",
                    Data = Encoding.UTF8.GetBytes("Hello, MQTT!"),
                    Parameters = new Dictionary<string, object>
                    {
                        { "topic", "test/topic" }
                    }
                };

                var publishResponse = await protocol.SendAsync(publishRequest);

                if (publishResponse.Success)
                {
                    Console.WriteLine("消息发布成功！");
                }
                else
                {
                    Console.WriteLine($"发布失败: {publishResponse.ErrorMessage}");
                }

                // 订阅主题
                Console.WriteLine("\n订阅主题...");
                var mqttProtocol = protocol as MqttProtocol;
                if (mqttProtocol != null)
                {
                    await mqttProtocol.SubscribeAsync("test/response", data =>
                    {
                        var message = Encoding.UTF8.GetString(data);
                        Console.WriteLine($"收到消息: {message}");
                    });

                    Console.WriteLine("已订阅主题: test/response");
                }

                // 等待一段时间接收消息
                Console.WriteLine("\n等待消息中...");
                await Task.Delay(5000);

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
        Console.WriteLine("\n=== MQTT 插件使用示例 ===");

        // 创建 MQTT 插件
        var plugin = new MqttPlugin();

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
            Address = "localhost",
            Port = 1883
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
    /// 发布/订阅模式示例
    /// </summary>
    public static async Task PubSubExample()
    {
        Console.WriteLine("\n=== MQTT 发布/订阅模式示例 ===");

        var protocol = new MqttProtocol();
        var config = new ProtocolConfig
        {
            Address = "localhost",
            Port = 1883
        };

        try
        {
            var connected = await protocol.ConnectAsync(config);
            if (!connected)
            {
                Console.WriteLine("连接失败！");
                return;
            }

            // 订阅多个主题
            var topics = new[] { "sensor/temperature", "sensor/humidity", "sensor/pressure" };

            foreach (var topic in topics)
            {
                await protocol.SubscribeAsync(topic, data =>
                {
                    var message = Encoding.UTF8.GetString(data);
                    Console.WriteLine($"[{topic}] 收到数据: {message}");
                });

                Console.WriteLine($"已订阅: {topic}");
            }

            // 模拟发布数据
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var temperature = 20 + random.NextDouble() * 10;
                var humidity = 40 + random.NextDouble() * 20;
                var pressure = 1000 + random.NextDouble() * 50;

                await PublishMessage(protocol, "sensor/temperature", temperature.ToString("F1"));
                await PublishMessage(protocol, "sensor/humidity", humidity.ToString("F1"));
                await PublishMessage(protocol, "sensor/pressure", pressure.ToString("F1"));

                await Task.Delay(1000);
            }

            await protocol.DisconnectAsync();
            Console.WriteLine("\n示例完成");
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

    private static async Task PublishMessage(MqttProtocol protocol, string topic, string message)
    {
        var request = new ProtocolRequest
        {
            CommandType = "Publish",
            Data = Encoding.UTF8.GetBytes(message),
            Parameters = new Dictionary<string, object>
            {
                { "topic", topic }
            }
        };

        await protocol.SendAsync(request);
    }

    /// <summary>
    /// 错误处理示例
    /// </summary>
    public static async Task ErrorHandlingExample()
    {
        Console.WriteLine("\n=== MQTT 错误处理示例 ===");

        var protocol = new MqttProtocol();

        // 订阅错误事件
        protocol.ErrorOccurred += (sender, args) =>
        {
            Console.WriteLine($"错误事件: {args.Message}");
        };

        // 尝试连接到不存在的代理
        var config = new ProtocolConfig
        {
            Address = "nonexistent-broker.example.com",
            Port = 1883,
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
