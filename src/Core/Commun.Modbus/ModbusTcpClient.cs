using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Commun.Modbus;

public class ModbusTcpClient : IDisposable
{
    private TcpClient? _client;
    private NetworkStream? _stream;
    private readonly ModbusConfig _config;
    private bool _disposed;

    public ModbusTcpClient(ModbusConfig config)
    {
        _config = config;
    }

    public async Task<bool> ConnectAsync()
    {
        try
        {
            _client = new TcpClient();
            await _client.ConnectAsync(_config.Host, _config.Port);
            _stream = _client.GetStream();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task DisconnectAsync()
    {
        if (_stream != null)
        {
            await _stream.DisposeAsync();
            _stream = null;
        }

        if (_client != null)
        {
            _client.Dispose();
            _client = null;
        }
    }

    public async Task<ModbusResponse> SendAsync(ModbusRequest request)
    {
        if (_client == null || _stream == null)
        {
            return new ModbusResponse { Success = false, ErrorMessage = "Not connected" };
        }

        try
        {
            var requestBytes = BuildRequest(request);
            await _stream.WriteAsync(requestBytes);

            var responseBytes = new byte[256];
            var bytesRead = await _stream.ReadAsync(responseBytes);

            return ParseResponse(responseBytes, bytesRead);
        }
        catch (Exception ex)
        {
            return new ModbusResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    private byte[] BuildRequest(ModbusRequest request)
    {
        var buffer = new byte[12];
        buffer[0] = 0x00; // Transaction ID high
        buffer[1] = 0x01; // Transaction ID low
        buffer[2] = 0x00; // Protocol ID high
        buffer[3] = 0x00; // Protocol ID low
        buffer[4] = 0x00; // Length high
        buffer[5] = 0x06; // Length low
        buffer[6] = _config.UnitId; // Unit ID
        buffer[7] = (byte)request.FunctionCode; // Function code
        buffer[8] = (byte)(request.StartAddress >> 8); // Start address high
        buffer[9] = (byte)(request.StartAddress & 0xFF); // Start address low
        buffer[10] = (byte)(request.Quantity >> 8); // Quantity high
        buffer[11] = (byte)(request.Quantity & 0xFF); // Quantity low

        return buffer;
    }

    private ModbusResponse ParseResponse(byte[] response, int length)
    {
        if (length < 9)
        {
            return new ModbusResponse { Success = false, ErrorMessage = "Invalid response length" };
        }

        var functionCode = response[7];

        if ((functionCode & 0x80) != 0)
        {
            return new ModbusResponse
            {
                Success = false,
                ExceptionCode = response[8],
                ErrorMessage = $"Modbus exception: {response[8]}"
            };
        }

        var dataLength = response[8];
        var data = new byte[dataLength];
        Array.Copy(response, 9, data, 0, dataLength);

        return new ModbusResponse
        {
            Success = true,
            Data = data
        };
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            DisconnectAsync().Wait();
            _disposed = true;
        }
    }
}
