namespace Database.Models;

public class SegmentModel
{
    public Guid? StatorId { get; set; }
    public double LocationX { get; set; }
    public double LocationY { get; set; }
    public bool Installed { get; set; }
    public int SegmentNo { get; set; }
}