using System;
using System.Threading.Tasks;

namespace Commun.Protocols;

public abstract class ProtocolBase : IProtocol
{
    public abstract string Name { get; }
    public abstract string Version { get; }
    public bool IsConnected { get; protected set; }

    protected ProtocolConfig? Config { get; private set; }

    public event EventHandler<ProtocolEventArgs>? DataReceived;
    public event EventHandler<ProtocolErrorEventArgs>? ErrorOccurred;

    public virtual async Task<bool> ConnectAsync(ProtocolConfig config)
    {
        Config = config;
        IsConnected = await ConnectCoreAsync(config);
        return IsConnected;
    }

    public virtual async Task DisconnectAsync()
    {
        await DisconnectCoreAsync();
        IsConnected = false;
    }

    public virtual async Task<ProtocolResponse> SendAsync(ProtocolRequest request)
    {
        if (!IsConnected)
        {
            return new ProtocolResponse
            {
                Success = false,
                ErrorMessage = "Not connected"
            };
        }

        return await SendCoreAsync(request);
    }

    protected abstract Task<bool> ConnectCoreAsync(ProtocolConfig config);
    protected abstract Task DisconnectCoreAsync();
    protected abstract Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request);

    protected virtual void OnDataReceived(byte[] data)
    {
        DataReceived?.Invoke(this, new ProtocolEventArgs(data));
    }

    protected virtual void OnError(string message, Exception? exception = null, int errorCode = 0)
    {
        ErrorOccurred?.Invoke(this, new ProtocolErrorEventArgs(message, exception, errorCode));
    }

    public virtual void Dispose()
    {
        if (IsConnected)
        {
            DisconnectAsync().Wait();
        }
    }
}
