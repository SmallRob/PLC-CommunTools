using System;
using System.Threading.Tasks;
using Commun.Protocols;

namespace Commun.Modbus;

public class ModbusProtocol : ProtocolBase
{
    public override string Name => "Modbus";
    public override string Version => "1.0.0";

    private ModbusTcpClient? _client;
    private ModbusConfig? _modbusConfig;

    protected override async Task<bool> ConnectCoreAsync(ProtocolConfig config)
    {
        _modbusConfig = new ModbusConfig
        {
            Host = config.Address,
            Port = config.Port,
            Timeout = config.Timeout,
            RetryCount = config.RetryCount
        };

        _client = new ModbusTcpClient(_modbusConfig);
        return await _client.ConnectAsync();
    }

    protected override async Task DisconnectCoreAsync()
    {
        if (_client != null)
        {
            await _client.DisconnectAsync();
            _client.Dispose();
            _client = null;
        }
    }

    protected override async Task<ProtocolResponse> SendCoreAsync(ProtocolRequest request)
    {
        if (_client == null)
        {
            return new ProtocolResponse { Success = false, ErrorMessage = "Client not initialized" };
        }

        var modbusRequest = new ModbusRequest
        {
            FunctionCode = ParseFunctionCode(request.CommandType),
            StartAddress = (ushort)request.Register,
            Quantity = (ushort)request.Length,
            Data = request.Data
        };

        var modbusResponse = await _client.SendAsync(modbusRequest);

        return new ProtocolResponse
        {
            Success = modbusResponse.Success,
            Data = modbusResponse.Data,
            ErrorMessage = modbusResponse.ErrorMessage,
            ErrorCode = modbusResponse.ExceptionCode
        };
    }

    private ModbusFunctionCode ParseFunctionCode(string commandType)
    {
        return commandType.ToLower() switch
        {
            "readcoils" => ModbusFunctionCode.ReadCoils,
            "readdiscreteinputs" => ModbusFunctionCode.ReadDiscreteInputs,
            "readholdingregisters" => ModbusFunctionCode.ReadHoldingRegisters,
            "readinputregisters" => ModbusFunctionCode.ReadInputRegisters,
            "writesinglecoil" => ModbusFunctionCode.WriteSingleCoil,
            "writesingleregister" => ModbusFunctionCode.WriteSingleRegister,
            "writemultiplecoils" => ModbusFunctionCode.WriteMultipleCoils,
            "writemultipleregisters" => ModbusFunctionCode.WriteMultipleRegisters,
            _ => throw new ArgumentException($"Unknown command type: {commandType}")
        };
    }
}
