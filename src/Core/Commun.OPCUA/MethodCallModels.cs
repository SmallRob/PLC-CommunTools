namespace Commun.OPCUA;

public class MethodCallResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public List<object> OutputArguments { get; set; } = new();
}

public class MethodCallRequest
{
    public string ObjectNodeId { get; set; } = string.Empty;
    public string MethodNodeId { get; set; } = string.Empty;
    public List<object> InputArguments { get; set; } = new();
}
