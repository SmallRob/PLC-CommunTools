namespace Commun.Modbus;

public class ModbusConfig
{
    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 502;
    public byte UnitId { get; set; } = 1;
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    public int RetryCount { get; set; } = 3;
    public bool UsePooling { get; set; } = true;
    public int MaxPoolSize { get; set; } = 10;
}
