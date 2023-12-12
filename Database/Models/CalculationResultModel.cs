namespace Database.Models;
// will be used for reporting 
// all of the values from database we want to use for reporting

public class CalculationResultModel
{
    public Guid SegmentId { get; set; }
    public int SegmentNo { get; set; }
    public int StatorNo { get; set; }
    public decimal MeasuredValue { get; set; }
    public decimal Tolerance { get; set; }
    public decimal? Deviation { get; set; }
    public DateTime Date { get; set; }
    public bool? Adjustment { get; set; }
}