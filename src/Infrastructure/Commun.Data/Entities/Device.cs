namespace Commun.Data.Entities;

public class Device
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Protocol { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public string? Config { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ConnectionHistory> ConnectionHistory { get; set; } = new List<ConnectionHistory>();
    public ICollection<DataRecord> DataRecords { get; set; } = new List<DataRecord>();
}
