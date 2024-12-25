namespace LargeMessageSubscriber.Domain.Settings
{
  public interface IInfluxDbSettings
  {
    public string? Token { get; set; }
    public string? Address { get; set; }
  }
}
