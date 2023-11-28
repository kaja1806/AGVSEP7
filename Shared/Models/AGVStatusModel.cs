using static Shared.Enums.StatusEnums;

namespace Shared.Models;

public class AgvStatusModel
{
    public SegmentStatus Status { get; set; }
    public string LogText { get; set; }
    public DateTime Timestamp { get; set; }
}