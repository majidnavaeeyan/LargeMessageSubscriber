using LargeMessageSubscriber.Domain.ViewModels;

namespace LargeMessageSubscriber.Domain.Services
{
  public interface IConfigurationService
  {
    IEnumerable<ErrorWarningModel> GetAllErrorTypes();
    IEnumerable<ErrorWarningModel> GetAllWarningTypes();
  }
}
