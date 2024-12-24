using LargeMessageSubscriber.Domain.ViewModels;

namespace LargeMessageSubscriber.Domain.Services
{
  public interface IConfigurationService
  {
    IEnumerable<ErrorWarningModel> GetAllErrorTypes();
    IEnumerable<ErrorWarningModel> GetAllWarningTypes();
    string GenerateJwtToken(TokenInputModel model);
    (bool, IEnumerable<int>, IEnumerable<int>) ValidateToken(string token);
  }
}
