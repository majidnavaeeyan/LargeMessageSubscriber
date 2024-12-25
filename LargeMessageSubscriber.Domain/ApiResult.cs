using System.Text;

namespace LargeMessageSubscriber.Domain
{
  public sealed class ApiResult<T>
  {
    public ApiResult(T result, IEnumerable<int> errorTypes, IEnumerable<int> warningTypes, string message = "")
    {
      Result = result;
      ErrorTypes = errorTypes;
      WarningTypes = warningTypes;
      Message = message;
      RequestId = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString()));
    }

    public T Result { get; set; }
    public IEnumerable<int> ErrorTypes { get; set; }
    public IEnumerable<int> WarningTypes { get; set; }
    public string Message { get; set; }
    public string RequestId { get; set; }
  }
}
