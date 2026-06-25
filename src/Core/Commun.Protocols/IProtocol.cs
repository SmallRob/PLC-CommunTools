using System;
using System.Threading.Tasks;

namespace Commun.Protocols;

public interface IProtocol : IDisposable
{
    string Name { get; }
    string Version { get; }
    bool IsConnected { get; }

    Task<bool> ConnectAsync(ProtocolConfig config);
    Task DisconnectAsync();
    Task<ProtocolResponse> SendAsync(ProtocolRequest request);

    event EventHandler<ProtocolEventArgs> DataReceived;
    event EventHandler<ProtocolErrorEventArgs> ErrorOccurred;
}
