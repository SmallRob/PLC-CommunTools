namespace Commun.Data.Entities;

public class ConnectionHistory
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public DateTime ConnectedAt { get; set; }
    public DateTime? DisconnectedAt { get; set; }
    public string Status { get; set; } = string.Empty;

    public Device Device { get; set; } = null!;
}
