using Commun.Modbus;
using Commun.Protocols;
using Xunit;

namespace Commun.Modbus.Tests;

public class ModbusProtocolTests
{
    [Fact]
    public void ModbusPlugin_HasCorrectMetadata()
    {
        var plugin = new ModbusPlugin();
        var metadata = plugin.GetMetadata();

        Assert.Equal("Modbus", metadata.Name);
        Assert.Equal("1.0.0", metadata.Version);
        Assert.Contains("ReadHoldingRegisters", metadata.SupportedCommands);
    }

    [Fact]
    public void ModbusPlugin_CreatesProtocolInstance()
    {
        var plugin = new ModbusPlugin();
        var protocol = plugin.CreateProtocol();

        Assert.NotNull(protocol);
        Assert.Equal("Modbus", protocol.Name);
    }

    [Fact]
    public void ModbusConfig_HasCorrectDefaults()
    {
        var config = new ModbusConfig();

        Assert.Equal("127.0.0.1", config.Host);
        Assert.Equal(502, config.Port);
        Assert.Equal(1, config.UnitId);
    }
}
