using Commun.Protocols;

namespace Commun.MQTT;

public class MqttPlugin : IProtocolPlugin
{
    public string ProtocolName => "MQTT";
    public string Version => "1.0.0";
    public string Description => "MQTT protocol implementation";

    public IProtocol CreateProtocol()
    {
        return new MqttProtocol();
    }

    public ProtocolMetadata GetMetadata()
    {
        return new ProtocolMetadata
        {
            Name = ProtocolName,
            Version = Version,
            Description = Description,
            Author = "PLC-CommunTools",
            SupportedCommands = new List<string>
            {
                "Publish",
                "Subscribe",
                "Unsubscribe"
            },
            DefaultConfig = new Dictionary<string, object>
            {
                { "Port", 1883 },
                { "KeepAlive", 60 },
                { "CleanSession", true }
            }
        };
    }
}
