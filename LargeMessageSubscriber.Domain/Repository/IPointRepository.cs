using LargeMessageSubscriber.Domain.DataModels;

namespace LargeMessageSubscriber.Domain.Repository
{
  public interface IPointRepository
  {
    Task InsertAsync(IEnumerable<Point> model);
  }
}