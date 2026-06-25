namespace Commun.Modbus;

public class ModbusRequest
{
    public ModbusFunctionCode FunctionCode { get; set; }
    public ushort StartAddress { get; set; }
    public ushort Quantity { get; set; }
    public byte[]? Data { get; set; }
}

public enum ModbusFunctionCode : byte
{
    ReadCoils = 0x01,
    ReadDiscreteInputs = 0x02,
    ReadHoldingRegisters = 0x03,
    ReadInputRegisters = 0x04,
    WriteSingleCoil = 0x05,
    WriteSingleRegister = 0x06,
    WriteMultipleCoils = 0x0F,
    WriteMultipleRegisters = 0x10
}
