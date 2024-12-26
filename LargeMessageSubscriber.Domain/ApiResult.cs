using System.Text;

namespace LargeMessageSubscriber.Domain
{
  public class ApiResult
  {
    public IEnumerable<int> ErrorTypes { get; set; }
    public IEnumerable<int> WarningTypes { get; set; }
    public string Message { get; set; }
    public string RequestId { get; set; }

    public ApiResult(IEnumerable<int> errorTypes, IEnumerable<int> warningTypes, string message = "")
    {
      ErrorTypes = errorTypes;
      WarningTypes = warningTypes;
      Message = message;
      RequestId = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString()));
    }
  }

  public sealed class ApiResult<T> : ApiResult
  {
    public T Result { get; set; }

    public ApiResult(T result, IEnumerable<int> errorTypes, IEnumerable<int> warningTypes, string message = "") : base(errorTypes, warningTypes, message)
    {
      Result = result;
    }
  }
}