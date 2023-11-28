using Shared.Models;

namespace AGV.Models;

public class AgvJsonFormatModel
{
    public Guid StatorID { get; set; }
    public List<SegmentFormatModel> Segment { get; set; }
}

public class SegmentFormatModel
{
    public int SegmentId { get; set; }
    public List<AgvStatusModel> Actions { get; set; }
}