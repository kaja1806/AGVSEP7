namespace Shared.Models;

public class AGVStatusModel
{
    public int AGVId { get; set; }
    public int Status { get; set; }
    public int Location { get; set; }
    public DateTime Timestamp { get; set; }
}