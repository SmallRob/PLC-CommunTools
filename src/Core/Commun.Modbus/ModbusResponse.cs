namespace Commun.Modbus;

public class ModbusResponse
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public byte ExceptionCode { get; set; }
    public ushort[]? RegisterValues { get; set; }
    public bool[]? CoilValues { get; set; }
}
