using System;
using System.Buffers;
using System.Threading.Tasks;
using Commun.Protocols;
using MQTTnet;

namespace Commun.MQTT;

public class MqttProtocol : ProtocolBase
{
    public override string Name => "MQTT";
    public override string Version => "1.0.0";

    private IMqttClient? _client;
    private MqttConfig? _mqttConfig;

    protected override async Task<bool> ConnectCoreAsync(ProtocolConfig config)
    {
        _mqttConfig = new MqttConfig
        {
            BrokerAddress = config.Address,
            Port = config.Port
        };

        var factory = new MqttClientFactory();
        _client = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttConfig.BrokerAddress, _mqttConfig.Port)
            .WithCredentials(_mqttConfig.Username, _mqttConfig.Password)
            .WithClientId(_mqttConfig.ClientId)
            .WithCleanSession(_mqttConfig.CleanSession)
            .Build();

        try
        {
            await _client.ConnectAsync(options);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected override async Task DisconnectCoreAsync()
    {
        if (_client != null)
        {
            if (_client.IsConnected)
            {
                var disconnectOptions = new MqttClientDisconnectOptionsBuilder()
                    .Build();
                await _client.DisconnectAsync(disconnectOptions);
            }
            _client.Dispose();
            _client = null;
        }
    }

    protected override async Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
    {
        if (_client == null || !_client.IsConnected)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = "Not connected" };
        }

        try
        {
            var topic = request.Parameters.ContainsKey("topic")
                ? request.Parameters["topic"].ToString()
                : "default";

            var payload = request.Data ?? Array.Empty<byte>();

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await _client.PublishAsync(message);

            return new ProtocolResponse { Success = true };
        }
        catch (Exception ex)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task SubscribeAsync(string topic, Action<byte[]> handler)
    {
        if (_client == null || !_client.IsConnected)
        {
            throw new InvalidOperationException("Not connected");
        }

        _client.ApplicationMessageReceivedAsync += e =>
        {
            var payload = e.ApplicationMessage.Payload;
            var payloadArray = payload.ToArray();
            handler(payloadArray);
            return Task.CompletedTask;
        };

        var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(topic)
            .Build();

        await _client.SubscribeAsync(subscribeOptions);
    }

    public override void Dispose()
    {
        if (_client != null)
        {
            if (_client.IsConnected)
            {
                var disconnectOptions = new MqttClientDisconnectOptionsBuilder().Build();
                _client.DisconnectAsync(disconnectOptions).Wait();
            }
            _client.Dispose();
            _client = null;
        }
        base.Dispose();
    }
}
