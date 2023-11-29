namespace Database.Models;

public class StatorModel
{
    public Guid Id { get; set; } //
    public string Name { get; set; }
    public string StatorNo { get; set; }
    public string ProductionOrder { get; set; }
    public int MeasurementCount { get; set; }
    public string Operator { get; set; }
    public DateTime Date { get; set; }
    public decimal NdeRadius { get; set; }
    public decimal MidRadius { get; set; }
    public decimal DeRadius { get; set; }
}