using Commun.Protocols;

namespace Commun.Serial;

public class SerialPlugin : IProtocolPlugin
{
    public string ProtocolName => "Serial";
    public string Version => "1.0.0";
    public string Description => "Serial port (RS232/RS485) communication protocol";

    public IProtocol CreateProtocol()
    {
        return new SerialProtocol();
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
                "Send",
                "Receive",
                "WriteString",
                "ReadExisting"
            },
            DefaultConfig = new Dictionary<string, object>
            {
                { "BaudRate", 9600 },
                { "DataBits", 8 },
                { "Parity", "None" },
                { "StopBits", "One" },
                { "ReadTimeout", 3000 }
            }
        };
    }
}
