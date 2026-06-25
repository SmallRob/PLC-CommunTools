namespace Commun.Protocols;

public class ProtocolConfig
{
    public string ProtocolName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    public int RetryCount { get; set; } = 3;
}
