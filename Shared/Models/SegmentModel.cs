namespace Shared.Models;

public class SegmentModel
{
    public Guid Id { get; set; }
    public Guid SegmentId{ get; set; }
    public float LocationX { get; set; }
    public float LocationY { get; set; }
    public float LocationH { get; set; }
    public int Status { get; set; }
    public decimal AnalysisResult { get; set; }
}
