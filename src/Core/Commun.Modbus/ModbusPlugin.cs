using Commun.Protocols;

namespace Commun.Modbus;

public class ModbusPlugin : IProtocolPlugin
{
    public string ProtocolName => "Modbus";
    public string Version => "1.0.0";
    public string Description => "Modbus TCP/RTU protocol implementation";

    public IProtocol CreateProtocol()
    {
        return new ModbusProtocol();
    }

    public ProtocolMetadata GetMetadata()
    {
        return new ProtocolMetadata
        {
            Name = ProtocolName,
            Version = Version,
            Description = Description,
            Author = "PLC-CommunTools",
            SupportedCommands = new List<string>
            {
                "ReadCoils",
                "ReadDiscreteInputs",
                "ReadHoldingRegisters",
                "ReadInputRegisters",
                "WriteSingleCoil",
                "WriteSingleRegister",
                "WriteMultipleCoils",
                "WriteMultipleRegisters"
            },
            DefaultConfig = new Dictionary<string, object>
            {
                { "Port", 502 },
                { "UnitId", 1 },
                { "Timeout", 30 }
            }
        };
    }
}
