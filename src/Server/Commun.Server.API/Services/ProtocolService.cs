using System.Collections.Concurrent;
using Commun.Protocols;
using Commun.Server.API.Models;

namespace Commun.Server.API.Services;

public class ProtocolService
{
    private readonly ProtocolPluginManager _pluginManager;
    private readonly ConcurrentDictionary<string, IProtocol> _activeProtocols = new();

    public ProtocolService(ProtocolPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
    }

    public IEnumerable<ProtocolMetadata> GetAvailableProtocols()
    {
        return _pluginManager.GetAvailableProtocols();
    }

    public async Task<bool> ConnectAsync(string deviceId, string protocolName, ProtocolConfig config)
    {
        if (_activeProtocols.ContainsKey(deviceId))
        {
            return true;
        }

        var protocol = _pluginManager.CreateProtocol(protocolName);
        var connected = await protocol.ConnectAsync(config);

        if (connected)
        {
            _activeProtocols[deviceId] = protocol;
        }
        else
        {
            protocol.Dispose();
        }

        return connected;
    }

    public async Task DisconnectAsync(string deviceId)
    {
        if (_activeProtocols.TryRemove(deviceId, out var protocol))
        {
            await protocol.DisconnectAsync();
            protocol.Dispose();
        }
    }

    public async Task<ProtocolResponse> SendAsync(string deviceId, ProtocolRequest request)
    {
        if (!_activeProtocols.TryGetValue(deviceId, out var protocol))
        {
            return new ProtocolResponse
            {
                Success = false,
                ErrorMessage = $"Device {deviceId} not connected"
            };
        }

        return await protocol.SendAsync(request);
    }

    public bool IsConnected(string deviceId)
    {
        return _activeProtocols.ContainsKey(deviceId);
    }
}
