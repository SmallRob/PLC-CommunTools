namespace Commun.MQTT;

public class MqttConfig
{
    public string BrokerAddress { get; set; } = "localhost";
    public int Port { get; set; } = 1883;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string ClientId { get; set; } = Guid.NewGuid().ToString();
    public bool UseTls { get; set; }
    public TimeSpan KeepAlive { get; set; } = TimeSpan.FromSeconds(60);
    public bool CleanSession { get; set; } = true;
}
