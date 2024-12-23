namespace LargeMessageSubscriber.Domain.Services
{
  public interface IPointService
  {
    Task InsertAsync(IEnumerable<DTOs.Point> model);
  }
}
