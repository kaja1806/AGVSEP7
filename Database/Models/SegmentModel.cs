using Shared.Models;
using static Shared.Enums.StatusEnums;

namespace Database.Models;

public class SegmentModel
{
    public Guid? Id { get; set; }
    public Guid? StatorId { get; set; }
    public Coordinates SegmentCoordinates { get; set; }
    public SegmentStatus? Status { get; set; }
    public int SegmentNo { get; set; }
}