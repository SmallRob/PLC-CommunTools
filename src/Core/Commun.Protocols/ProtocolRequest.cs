namespace Commun.Protocols;

public class ProtocolRequest
{
    public string CommandType { get; set; } = string.Empty;
    public int Register { get; set; }
    public int Length { get; set; } = 1;
    public byte[]? Data { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}
