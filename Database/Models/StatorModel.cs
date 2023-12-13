namespace Database.Models;

public class StatorModel
{
    public Guid Id { get; set; } //
    public string Name { get; set; }
    public string StatorNo { get; set; }
    public string ProductionOrder { get; set; }
    public string Operator { get; set; }
    public DateTime Date { get; set; }
    public int MeasurementNo { get; set; }
    public double StatorTemp { get; set; }
    public bool Finished { get; set; }
}