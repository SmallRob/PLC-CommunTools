namespace Commun.Server.API.Models;

public class ProtocolRequestDto
{
    public string CommandType { get; set; } = string.Empty;
    public int Register { get; set; }
    public int Length { get; set; } = 1;
    public byte[]? Data { get; set; }
    public Dictionary<string, object>? Parameters { get; set; }
}

public class ConnectRequest
{
    public string Protocol { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
}
