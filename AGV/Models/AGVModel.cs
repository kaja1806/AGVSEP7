using Shared.Models;

namespace AGV.Models;

public class AgvModel
{
    public int SegmentNo { get; set; }
    public Coordinates Coordinates { get; set; }
    public string Action { get; set; }
    public DateTime AddedAt { get; set; }
}