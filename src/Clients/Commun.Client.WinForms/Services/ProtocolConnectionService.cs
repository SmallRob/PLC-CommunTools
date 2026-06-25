using Commun.Protocols;
using Commun.Modbus;
using Commun.MQTT;
using Commun.Serial;
using Commun.OPCUA;

namespace Commun.Client.WinForms.Services;

public class ProtocolConnectionService
{
    private readonly Dictionary<string, IProtocol> _connections = new();
    private readonly ProtocolPluginManager _pluginManager;

    public event EventHandler<string>? ConnectionStateChanged;
    public event EventHandler<(string connectionId, byte[] data)>? DataReceived;

    public ProtocolConnectionService()
    {
        _pluginManager = new ProtocolPluginManager("plugins");
    }

    public IReadOnlyDictionary<string, IProtocol> Connections => _connections;

    public async Task<bool> ConnectAsync(string connectionId, string protocolName, ProtocolConfig config)
    {
        if (_connections.ContainsKey(connectionId))
        {
            await DisconnectAsync(connectionId);
        }

        IProtocol protocol = protocolName.ToLower() switch
        {
            "modbus" => new ModbusProtocol(),
            "mqtt" => new MqttProtocol(),
            "serial" => new SerialProtocol(),
            "opcua" => new OpcUaProtocol(),
            _ => throw new ArgumentException($"Unknown protocol: {protocolName}")
        };

        protocol.DataReceived += (s, e) => DataReceived?.Invoke(connectionId, (connectionId, e.Data));
        protocol.ErrorOccurred += (s, e) => ConnectionStateChanged?.Invoke(this, $"Error: {e.Message}");

        var connected = await protocol.ConnectAsync(config);
        if (connected)
        {
            _connections[connectionId] = protocol;
            ConnectionStateChanged?.Invoke(this, $"Connected: {connectionId}");
        }

        return connected;
    }

    public async Task DisconnectAsync(string connectionId)
    {
        if (_connections.TryGetValue(connectionId, out var protocol))
        {
            await protocol.DisconnectAsync();
            protocol.Dispose();
            _connections.Remove(connectionId);
            ConnectionStateChanged?.Invoke(this, $"Disconnected: {connectionId}");
        }
    }

    public IProtocol? GetConnection(string connectionId)
    {
        return _connections.TryGetValue(connectionId, out var protocol) ? protocol : null;
    }

    public async Task DisconnectAllAsync()
    {
        foreach (var id in _connections.Keys.ToList())
        {
            await DisconnectAsync(id);
        }
    }
}
