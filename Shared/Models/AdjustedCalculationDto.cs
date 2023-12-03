namespace Shared.Models;

public class AdjustedCalculationDto
{
    public DateTime Date { get; set; }
    public double MeasuredValue { get; set; }
    public double Tolerance { get; set; }
    public double Deviation { get; set; }
    public bool AdjustmentNeeded { get; set; }
    public int SegmentNo { get; set; }
    public string? StatorNo { get; set; }
}