namespace LargeMessageSubscriber.Domain.ViewModels
{
  public class QueryResult
  {
    public DateTime? Time { get; set; }
    public object? Value { get; set; }
    public object? Field { get; set; }
    public string? Measurement { get; set; }
  }
}
