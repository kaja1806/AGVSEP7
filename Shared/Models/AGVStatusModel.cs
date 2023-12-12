using static Shared.Enums.StatusEnums;

namespace Shared.Models;

public class AgvStatusModel
{
    public AGVStatus Status { get; set; }
    public string LogText { get; set; }
    public DateTime Timestamp { get; set; }
    
    public int SegmentNo { get; set; }
    public Coordinates Coordinates { get; set; }
    public string Action { get; set; }
    public DateTime AddedAt { get; set; }
}