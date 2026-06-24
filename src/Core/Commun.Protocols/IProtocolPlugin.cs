namespace Commun.Protocols;

public interface IProtocolPlugin
{
    string ProtocolName { get; }
    string Version { get; }
    string Description { get; }

    IProtocol CreateProtocol();
    ProtocolMetadata GetMetadata();
}

public class ProtocolMetadata
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public List<string> SupportedCommands { get; set; } = new();
    public Dictionary<string, object> DefaultConfig { get; set; } = new();
}
