using Commun.OPCUA;
using Commun.Protocols;
using Xunit;

namespace Commun.OPCUA.Tests;

public class OpcUaProtocolTests
{
    [Fact]
    public void OpcUaPlugin_HasCorrectMetadata()
    {
        var plugin = new OpcUaPlugin();
        var metadata = plugin.GetMetadata();

        Assert.Equal("OPC UA", metadata.Name);
        Assert.Equal("1.0.0", metadata.Version);
        Assert.Contains("Read", metadata.SupportedCommands);
        Assert.Contains("Write", metadata.SupportedCommands);
        Assert.Contains("Browse", metadata.SupportedCommands);
    }

    [Fact]
    public void OpcUaPlugin_CreatesProtocolInstance()
    {
        var plugin = new OpcUaPlugin();
        var protocol = plugin.CreateProtocol();

        Assert.NotNull(protocol);
        Assert.Equal("OPC UA", protocol.Name);
        Assert.Equal("1.0.0", protocol.Version);
    }

    [Fact]
    public void OpcUaPlugin_DefaultConfig_HasCorrectValues()
    {
        var plugin = new OpcUaPlugin();
        var metadata = plugin.GetMetadata();

        Assert.Equal(4840, metadata.DefaultConfig["Port"]);
        Assert.Equal(false, metadata.DefaultConfig["UseSecurity"]);
        Assert.Equal(true, metadata.DefaultConfig["AutoAccept"]);
        Assert.Equal(60000, metadata.DefaultConfig["SessionTimeout"]);
    }

    [Fact]
    public void OpcUaConfig_HasCorrectDefaults()
    {
        var config = new OpcUaConfig();

        Assert.Equal("opc.tcp://localhost:4840", config.EndpointUrl);
        Assert.Equal("PLC-CommunTools", config.ApplicationName);
        Assert.True(config.AutoAccept);
        Assert.Equal(60000, config.SessionTimeout);
        Assert.Equal(15000, config.OperationTimeout);
        Assert.False(config.UseSecurity);
        Assert.Null(config.Username);
        Assert.Null(config.Password);
    }

    [Fact]
    public async Task ConnectAsync_InvalidEndpoint_ReturnsFalse()
    {
        var protocol = new OpcUaProtocol();
        var errorCalled = false;

        protocol.ErrorOccurred += (s, e) => { errorCalled = true; };

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1", // TEST-NET, non-routable
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(3)
        };

        var result = await protocol.ConnectAsync(config);

        Assert.False(result);
        Assert.False(protocol.IsConnected);
        Assert.True(errorCalled);

        protocol.Dispose();
    }

    [Fact]
    public async Task ConnectAsync_InvalidEndpoint_FiresErrorEvent()
    {
        var protocol = new OpcUaProtocol();
        string? errorMessage = null;

        protocol.ErrorOccurred += (s, e) => { errorMessage = e.Message; };

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1",
            Port = 9999,
            Timeout = TimeSpan.FromSeconds(2)
        };

        await protocol.ConnectAsync(config);

        Assert.NotNull(errorMessage);
        Assert.Contains("Failed to connect", errorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task SendAsync_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var request = new ProtocolRequest
        {
            CommandType = "Read",
            Register = 1001
        };

        var response = await protocol.SendAsync(request);

        Assert.False(response.Success);
        Assert.Equal("Not connected", response.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task ReadNodeAsync_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var response = await protocol.ReadNodeAsync("ns=2;s=Demo.Static.Scalar.Int32");

        Assert.False(response.Success);
        Assert.Equal("Not connected", response.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task WriteNodeAsync_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var response = await protocol.WriteNodeAsync("ns=2;s=Demo.Static.Scalar.Int32", 12345);

        Assert.False(response.Success);
        Assert.Equal("Not connected", response.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task BrowseAsync_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var response = await protocol.BrowseAsync("ns=2;s=Demo");

        Assert.False(response.Success);
        Assert.Equal("Not connected", response.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task DisconnectAsync_NotConnected_DoesNotThrow()
    {
        var protocol = new OpcUaProtocol();

        await protocol.DisconnectAsync();

        Assert.False(protocol.IsConnected);

        protocol.Dispose();
    }

    [Fact]
    public async Task ConnectThenDisconnect_StateIsCorrect()
    {
        var protocol = new OpcUaProtocol();

        Assert.False(protocol.IsConnected);

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(2)
        };

        var connected = await protocol.ConnectAsync(config);
        Assert.False(connected);
        Assert.False(protocol.IsConnected);

        await protocol.DisconnectAsync();
        Assert.False(protocol.IsConnected);

        protocol.Dispose();
    }

    [Fact]
    public async Task MultipleDisconnects_DoesNotThrow()
    {
        var protocol = new OpcUaProtocol();

        await protocol.DisconnectAsync();
        await protocol.DisconnectAsync();
        await protocol.DisconnectAsync();

        Assert.False(protocol.IsConnected);

        protocol.Dispose();
    }

    [Fact]
    public void Dispose_MultipleTimes_DoesNotThrow()
    {
        var protocol = new OpcUaProtocol();

        protocol.Dispose();
        protocol.Dispose();
    }

    [Fact]
    public async Task SendAsync_AfterFailedConnect_ReturnsNotConnected()
    {
        var protocol = new OpcUaProtocol();

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(2)
        };

        await protocol.ConnectAsync(config);

        var request = new ProtocolRequest
        {
            CommandType = "Read",
            Register = 1001
        };

        var response = await protocol.SendAsync(request);

        Assert.False(response.Success);
        Assert.Equal("Not connected", response.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public void OpcUaProtocol_ImplementsIProtocol()
    {
        var protocol = new OpcUaProtocol();

        Assert.IsAssignableFrom<IProtocol>(protocol);
        Assert.IsAssignableFrom<IDisposable>(protocol);

        protocol.Dispose();
    }

    [Fact]
    public void OpcUaPlugin_ImplementsIProtocolPlugin()
    {
        var plugin = new OpcUaPlugin();

        Assert.IsAssignableFrom<IProtocolPlugin>(plugin);
    }

    [Fact]
    public void OpcUaPlugin_HasSubscribeCommand()
    {
        var plugin = new OpcUaPlugin();
        var metadata = plugin.GetMetadata();

        Assert.Contains("Subscribe", metadata.SupportedCommands);
        Assert.Contains("Unsubscribe", metadata.SupportedCommands);
    }

    [Fact]
    public void Subscribe_NotConnected_ReturnsFalse()
    {
        var protocol = new OpcUaProtocol();

        var result = protocol.Subscribe("ns=2;s=Demo.Static.Scalar.Int32");

        Assert.False(result);

        protocol.Dispose();
    }

    [Fact]
    public void Unsubscribe_NotConnected_ReturnsFalse()
    {
        var protocol = new OpcUaProtocol();

        var result = protocol.Unsubscribe("ns=2;s=Demo.Static.Scalar.Int32");

        Assert.False(result);

        protocol.Dispose();
    }

    [Fact]
    public void IsSubscribed_NotConnected_ReturnsFalse()
    {
        var protocol = new OpcUaProtocol();

        var result = protocol.IsSubscribed("ns=2;s=Demo.Static.Scalar.Int32");

        Assert.False(result);

        protocol.Dispose();
    }

    [Fact]
    public void GetActiveSubscriptions_NotConnected_ReturnsEmpty()
    {
        var protocol = new OpcUaProtocol();

        var subscriptions = protocol.GetActiveSubscriptions();

        Assert.NotNull(subscriptions);
        Assert.Empty(subscriptions);

        protocol.Dispose();
    }

    [Fact]
    public void SubscriptionCount_NotConnected_ReturnsZero()
    {
        var protocol = new OpcUaProtocol();

        Assert.Equal(0, protocol.SubscriptionCount);

        protocol.Dispose();
    }

    [Fact]
    public async Task Subscribe_AfterFailedConnect_ReturnsFalse()
    {
        var protocol = new OpcUaProtocol();

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(2)
        };

        await protocol.ConnectAsync(config);

        var result = protocol.Subscribe("ns=2;s=Demo.Static.Scalar.Int32");

        Assert.False(result);

        protocol.Dispose();
    }

    [Fact]
    public async Task SubscriptionCount_AfterFailedConnect_ReturnsZero()
    {
        var protocol = new OpcUaProtocol();

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(2)
        };

        await protocol.ConnectAsync(config);

        Assert.Equal(0, protocol.SubscriptionCount);

        protocol.Dispose();
    }

    [Fact]
    public void NodeValueChanged_Event_CanSubscribe()
    {
        var protocol = new OpcUaProtocol();

        NodeValueChangedEventArgs? receivedArgs = null;
        protocol.NodeValueChanged += (sender, args) => { receivedArgs = args; };

        Assert.Null(receivedArgs);

        protocol.Dispose();
    }

    [Fact]
    public void NodeValueChanged_Event_CanUnsubscribe()
    {
        var protocol = new OpcUaProtocol();

        void Handler(object? sender, NodeValueChangedEventArgs args) { }
        protocol.NodeValueChanged += Handler;
        protocol.NodeValueChanged -= Handler;

        protocol.Dispose();
    }

    [Fact]
    public void OpcUaPlugin_HasCallCommand()
    {
        var plugin = new OpcUaPlugin();
        var metadata = plugin.GetMetadata();

        Assert.Contains("Call", metadata.SupportedCommands);
    }

    [Fact]
    public async Task CallMethodAsync_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var result = await protocol.CallMethodAsync("ns=2;s=Object", "ns=2;s=Method", 1, 2, 3);

        Assert.False(result.Success);
        Assert.Equal("Not connected", result.ErrorMessage);
        Assert.Empty(result.OutputArguments);

        protocol.Dispose();
    }

    [Fact]
    public async Task CallMethodAsync_VariantArgs_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var args = new[] { new Opc.Ua.Variant(1), new Opc.Ua.Variant("test") };
        var result = await protocol.CallMethodAsync("ns=2;s=Object", "ns=2;s=Method", args);

        Assert.False(result.Success);
        Assert.Equal("Not connected", result.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task CallMethodAsync_Request_NotConnected_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var request = new MethodCallRequest
        {
            ObjectNodeId = "ns=2;s=Object",
            MethodNodeId = "ns=2;s=Method",
            InputArguments = new List<object> { 1, "hello", 3.14 }
        };

        var result = await protocol.CallMethodAsync(request);

        Assert.False(result.Success);
        Assert.Equal("Not connected", result.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public async Task CallMethodAsync_AfterFailedConnect_ReturnsError()
    {
        var protocol = new OpcUaProtocol();

        var config = new ProtocolConfig
        {
            Address = "192.0.2.1",
            Port = 4840,
            Timeout = TimeSpan.FromSeconds(2)
        };

        await protocol.ConnectAsync(config);

        var result = await protocol.CallMethodAsync("ns=2;s=Object", "ns=2;s=Method", 1);

        Assert.False(result.Success);
        Assert.Equal("Not connected", result.ErrorMessage);

        protocol.Dispose();
    }

    [Fact]
    public void MethodCallResult_DefaultValues()
    {
        var result = new MethodCallResult();

        Assert.False(result.Success);
        Assert.Null(result.ErrorMessage);
        Assert.NotNull(result.OutputArguments);
        Assert.Empty(result.OutputArguments);
    }

    [Fact]
    public void MethodCallRequest_DefaultValues()
    {
        var request = new MethodCallRequest();

        Assert.Equal(string.Empty, request.ObjectNodeId);
        Assert.Equal(string.Empty, request.MethodNodeId);
        Assert.NotNull(request.InputArguments);
        Assert.Empty(request.InputArguments);
    }

    [Fact]
    public void MethodCallRequest_CanSetProperties()
    {
        var request = new MethodCallRequest
        {
            ObjectNodeId = "ns=2;s=MyObject",
            MethodNodeId = "ns=2;s=MyMethod",
            InputArguments = new List<object> { 42, "hello", true }
        };

        Assert.Equal("ns=2;s=MyObject", request.ObjectNodeId);
        Assert.Equal("ns=2;s=MyMethod", request.MethodNodeId);
        Assert.Equal(3, request.InputArguments.Count);
    }

    [Fact]
    public void MethodCallResult_CanSetProperties()
    {
        var result = new MethodCallResult
        {
            Success = true,
            OutputArguments = new List<object> { 100, "result" }
        };

        Assert.True(result.Success);
        Assert.Equal(2, result.OutputArguments.Count);
    }
}
