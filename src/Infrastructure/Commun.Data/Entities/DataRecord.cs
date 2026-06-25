namespace Commun.Data.Entities;

public class DataRecord
{
    public long Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public int Register { get; set; }
    public string Value { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Device Device { get; set; } = null!;
}
