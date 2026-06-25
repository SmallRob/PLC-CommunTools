namespace Commun.Protocols;

public class ProtocolResponse
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public int ErrorCode { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
}
