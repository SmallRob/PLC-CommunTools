using Commun.MQTT;
using Commun.Protocols;
using Xunit;

namespace Commun.MQTT.Tests;

public class MqttProtocolTests
{
    [Fact]
    public void MqttPlugin_HasCorrectMetadata()
    {
        var plugin = new MqttPlugin();
        var metadata = plugin.GetMetadata();

        Assert.Equal("MQTT", metadata.Name);
        Assert.Equal("1.0.0", metadata.Version);
        Assert.Contains("Publish", metadata.SupportedCommands);
    }

    [Fact]
    public void MqttPlugin_CreatesProtocolInstance()
    {
        var plugin = new MqttPlugin();
        var protocol = plugin.CreateProtocol();

        Assert.NotNull(protocol);
        Assert.Equal("MQTT", protocol.Name);
    }

    [Fact]
    public void MqttConfig_HasCorrectDefaults()
    {
        var config = new MqttConfig();

        Assert.Equal("localhost", config.BrokerAddress);
        Assert.Equal(1883, config.Port);
        Assert.True(config.CleanSession);
    }
}
