using Commun.Protocols;
using Xunit;

namespace Commun.Protocols.Tests;

public class ProtocolTests
{
    [Fact]
    public void ProtocolConfig_DefaultValues_AreCorrect()
    {
        var config = new ProtocolConfig();

        Assert.Equal(string.Empty, config.ProtocolName);
        Assert.Equal(string.Empty, config.Address);
        Assert.Equal(0, config.Port);
        Assert.Empty(config.Parameters);
        Assert.Equal(TimeSpan.FromSeconds(30), config.Timeout);
        Assert.Equal(3, config.RetryCount);
    }

    [Fact]
    public void ProtocolRequest_DefaultValues_AreCorrect()
    {
        var request = new ProtocolRequest();

        Assert.Equal(string.Empty, request.CommandType);
        Assert.Equal(0, request.Register);
        Assert.Equal(1, request.Length);
        Assert.Null(request.Data);
        Assert.Empty(request.Parameters);
    }

    [Fact]
    public void ProtocolResponse_DefaultValues_AreCorrect()
    {
        var response = new ProtocolResponse();

        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.Null(response.ErrorMessage);
        Assert.Equal(0, response.ErrorCode);
        Assert.Empty(response.Metadata);
    }

    [Fact]
    public void ProtocolEventArgs_StoresData()
    {
        var data = new byte[] { 1, 2, 3 };
        var args = new ProtocolEventArgs(data);

        Assert.Equal(data, args.Data);
        Assert.True(args.Timestamp <= DateTime.UtcNow);
    }

    [Fact]
    public void ProtocolErrorEventArgs_StoresProperties()
    {
        var ex = new Exception("test");
        var args = new ProtocolErrorEventArgs("error", ex, 42);

        Assert.Equal("error", args.Message);
        Assert.Equal(ex, args.Exception);
        Assert.Equal(42, args.ErrorCode);
    }

    [Fact]
    public void ProtocolErrorEventArgs_NullException_IsAllowed()
    {
        var args = new ProtocolErrorEventArgs("error");

        Assert.Equal("error", args.Message);
        Assert.Null(args.Exception);
        Assert.Equal(0, args.ErrorCode);
    }

    [Fact]
    public void ProtocolMetadata_DefaultValues_AreCorrect()
    {
        var metadata = new ProtocolMetadata();

        Assert.Equal(string.Empty, metadata.Name);
        Assert.Equal(string.Empty, metadata.Version);
        Assert.Equal(string.Empty, metadata.Description);
        Assert.Equal(string.Empty, metadata.Author);
        Assert.Empty(metadata.SupportedCommands);
        Assert.Empty(metadata.DefaultConfig);
    }

    [Fact]
    public void ProtocolPluginManager_NonExistentDirectory_CreatesDirectory()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        try
        {
            var manager = new ProtocolPluginManager(tempDir);
            manager.LoadPlugins();

            Assert.True(Directory.Exists(tempDir));
            Assert.Empty(manager.GetAvailableProtocols());
        }
        finally
        {
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Fact]
    public void ProtocolPluginManager_HasProtocol_ReturnsFalseForUnknown()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        try
        {
            var manager = new ProtocolPluginManager(tempDir);
            manager.LoadPlugins();

            Assert.False(manager.HasProtocol("Unknown"));
        }
        finally
        {
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Fact]
    public void ProtocolPluginManager_CreateProtocol_ThrowsForUnknown()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        try
        {
            var manager = new ProtocolPluginManager(tempDir);
            manager.LoadPlugins();

            Assert.Throws<ArgumentException>(() => manager.CreateProtocol("Unknown"));
        }
        finally
        {
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }

    [Fact]
    public async Task ProtocolBase_SendAsync_NotConnected_ReturnsError()
    {
        using var protocol = new TestProtocol();

        var response = await protocol.SendAsync(new ProtocolRequest());

        Assert.False(response.Success);
        Assert.Equal("Not connected", response.ErrorMessage);
    }

    [Fact]
    public async Task ProtocolBase_ConnectAsync_SetsIsConnected()
    {
        using var protocol = new TestProtocol();
        var config = new ProtocolConfig { Address = "127.0.0.1", Port = 502 };

        var result = await protocol.ConnectAsync(config);

        Assert.True(result);
        Assert.True(protocol.IsConnected);
    }

    [Fact]
    public async Task ProtocolBase_DisconnectAsync_ClearsIsConnected()
    {
        using var protocol = new TestProtocol();
        await protocol.ConnectAsync(new ProtocolConfig());

        await protocol.DisconnectAsync();

        Assert.False(protocol.IsConnected);
    }

    [Fact]
    public async Task ProtocolBase_SendAsync_Conconnected_DelegatesToCore()
    {
        using var protocol = new TestProtocol();
        await protocol.ConnectAsync(new ProtocolConfig());

        var response = await protocol.SendAsync(new ProtocolRequest { CommandType = "Read" });

        Assert.True(response.Success);
        Assert.Equal(new byte[] { 0xAA }, response.Data);
    }

    [Fact]
    public void ProtocolBase_OnDataReceived_RaisesEvent()
    {
        using var protocol = new TestProtocol();
        ProtocolEventArgs? receivedArgs = null;
        protocol.DataReceived += (_, e) => receivedArgs = e;

        protocol.RaiseDataReceived(new byte[] { 1, 2, 3 });

        Assert.NotNull(receivedArgs);
        Assert.Equal(new byte[] { 1, 2, 3 }, receivedArgs!.Data);
    }

    [Fact]
    public void ProtocolBase_OnError_RaisesEvent()
    {
        using var protocol = new TestProtocol();
        ProtocolErrorEventArgs? errorArgs = null;
        protocol.ErrorOccurred += (_, e) => errorArgs = e;

        protocol.RaiseError("test error", null, 99);

        Assert.NotNull(errorArgs);
        Assert.Equal("test error", errorArgs!.Message);
        Assert.Equal(99, errorArgs.ErrorCode);
    }

    private class TestProtocol : ProtocolBase
    {
        public override string Name => "Test";
        public override string Version => "1.0";

        protected override Task<bool> ConnectCoreAsync(ProtocolConfig config)
        {
            return Task.FromResult(true);
        }

        protected override Task DisconnectCoreAsync()
        {
            return Task.CompletedTask;
        }

        protected override Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
        {
            return Task.FromResult(new ProtocolResponse
            {
                Success = true,
                Data = new byte[] { 0xAA }
            });
        }

        public void RaiseDataReceived(byte[] data) => OnDataReceived(data);
        public void RaiseError(string msg, Exception? ex, int code) => OnError(msg, ex, code);
    }
}
