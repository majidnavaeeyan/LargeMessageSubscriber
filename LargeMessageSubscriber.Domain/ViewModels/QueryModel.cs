namespace LargeMessageSubscriber.Domain.ViewModels
{
  public class QueryModel
  {
    public string MeasurementName { get; set; }
    public string FieldName { get; set; }
    public string Precision { get; set; } // "raw", "hourly", or "daily"
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
  }
}
