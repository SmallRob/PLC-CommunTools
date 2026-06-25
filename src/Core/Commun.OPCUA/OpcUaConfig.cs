namespace Commun.OPCUA;

public class OpcUaConfig
{
    public string EndpointUrl { get; set; } = "opc.tcp://localhost:4840";
    public string ApplicationName { get; set; } = "PLC-CommunTools";
    public bool AutoAccept { get; set; } = true;
    public int SessionTimeout { get; set; } = 60000;
    public int OperationTimeout { get; set; } = 15000;
    public bool UseSecurity { get; set; } = false;
    public string? Username { get; set; }
    public string? Password { get; set; }
}
