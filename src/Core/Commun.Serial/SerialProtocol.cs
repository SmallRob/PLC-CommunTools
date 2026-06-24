using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Commun.Protocols;

namespace Commun.Serial;

public class SerialProtocol : ProtocolBase
{
    public override string Name => "Serial";
    public override string Version => "1.0.0";

    private SerialPort? _port;
    private SerialConfig? _config;

    protected override Task<bool> ConnectCoreAsync(ProtocolConfig config)
    {
        _config = new SerialConfig
        {
            PortName = config.Address,
            BaudRate = config.Port > 0 ? config.Port : 9600,
            ReadTimeout = (int)config.Timeout.TotalMilliseconds,
            WriteTimeout = (int)config.Timeout.TotalMilliseconds
        };

        if (config.Parameters.TryGetValue("DataBits", out var dataBits))
            _config.DataBits = Convert.ToInt32(dataBits);
        if (config.Parameters.TryGetValue("Parity", out var parity))
            _config.Parity = parity.ToString() ?? "None";
        if (config.Parameters.TryGetValue("StopBits", out var stopBits))
            _config.StopBits = stopBits.ToString() ?? "One";

        try
        {
            _port = new SerialPort
            {
                PortName = _config.PortName,
                BaudRate = _config.BaudRate,
                DataBits = _config.DataBits,
                Parity = Enum.Parse<Parity>(_config.Parity),
                StopBits = Enum.Parse<StopBits>(_config.StopBits),
                Handshake = Enum.Parse<Handshake>(_config.Handshake),
                ReadTimeout = _config.ReadTimeout,
                WriteTimeout = _config.WriteTimeout
            };

            _port.DataReceived += OnDataReceived;
            _port.Open();
            return Task.FromResult(_port.IsOpen);
        }
        catch (Exception ex)
        {
            OnError($"Failed to open serial port: {ex.Message}", ex);
            return Task.FromResult(false);
        }
    }

    protected override Task DisconnectCoreAsync()
    {
        if (_port != null)
        {
            _port.DataReceived -= OnDataReceived;
            if (_port.IsOpen) _port.Close();
            _port.Dispose();
            _port = null;
        }
        return Task.CompletedTask;
    }

    protected override Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
    {
        if (_port == null || !_port.IsOpen)
        {
            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = "Port not open" });
        }

        try
        {
            if (request.Data != null)
            {
                _port.Write(request.Data, 0, request.Data.Length);
                return Task.FromResult(new ProtocolResponse { Success = true });
            }

            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = "No data to send" });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = ex.Message });
        }
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            var bytesToRead = _port!.BytesToRead;
            var buffer = new byte[bytesToRead];
            _port.Read(buffer, 0, bytesToRead);
            OnDataReceived(buffer);
        }
        catch (Exception ex)
        {
            OnError($"Error reading serial data: {ex.Message}", ex);
        }
    }

    public Task<ProtocolResponse> WriteStringAsync(string data)
    {
        if (_port == null || !_port.IsOpen)
        {
            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = "Port not open" });
        }

        try
        {
            _port.Write(data);
            return Task.FromResult(new ProtocolResponse { Success = true });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = ex.Message });
        }
    }

    public Task<ProtocolResponse> ReadExistingAsync()
    {
        if (_port == null || !_port.IsOpen)
        {
            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = "Port not open" });
        }

        try
        {
            var data = _port.ReadExisting();
            return Task.FromResult(new ProtocolResponse
            {
                Success = true,
                Data = System.Text.Encoding.UTF8.GetBytes(data)
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new ProtocolResponse { Success = false, ErrorMessage = ex.Message });
        }
    }

    public static string[] GetAvailablePorts()
    {
        return SerialPort.GetPortNames();
    }
}
