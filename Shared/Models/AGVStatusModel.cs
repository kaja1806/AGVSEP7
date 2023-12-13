namespace Shared.Models;

public class AgvStatusModel
{
    public int SegmentNo { get; set; }
    public DateTime AddedAt { get; set; }
    public string LogText { get; set; }
    public int StatorNo { get; set; }
}