using Commun.Protocols;

namespace Commun.OPCUA;

public class OpcUaPlugin : IProtocolPlugin
{
    public string ProtocolName => "OPC UA";
    public string Version => "1.0.0";
    public string Description => "OPC UA (Open Platform Communications Unified Architecture) protocol";

    public IProtocol CreateProtocol()
    {
        return new OpcUaProtocol();
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
                "Read",
                "Write",
                "Browse",
                "Subscribe",
                "Unsubscribe",
                "Call"
            },
            DefaultConfig = new Dictionary<string, object>
            {
                { "Port", 4840 },
                { "UseSecurity", false },
                { "AutoAccept", true },
                { "SessionTimeout", 60000 }
            }
        };
    }
}
