using LargeMessageSubscriber.Domain.DTOs;
using LargeMessageSubscriber.Domain.ViewModels;

namespace LargeMessageSubscriber.Domain.Services
{
  public interface IPointService
  {
    Task InsertAsync(IEnumerable<DTOs.Point> model);
    Task<IEnumerable<QueryResult>> GetAsync(QueryModel model);
    Task InsertRecievedMessagesToDbAsync();
    Task EnqueuMessageToMessageBrokerAsync(List<DTOs.Point> model);
  }
}
