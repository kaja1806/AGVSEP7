namespace Shared.Models;

public class AdjustedCalculationDto
{
    public DateTime Date { get; set; }
    public decimal MeasuredValue { get; set; }
    public decimal Tolerance { get; set; }
    public decimal? Deviation { get; set; }
    public bool? AdjustmentNeeded { get; set; }
    public int SegmentNo { get; set; }
    public int? StatorNo { get; set; }
}