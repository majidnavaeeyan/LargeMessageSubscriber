using LargeMessageSubscriber.Domain.ViewModels;

namespace LargeMessageSubscriber.Domain.Repository
{
  public interface IPointRepository
  {
    Task InsertAsync(IEnumerable<DataModels.Point> model);
    Task<IEnumerable<QueryResult>> GetAsync(QueryModel model);
  }
}