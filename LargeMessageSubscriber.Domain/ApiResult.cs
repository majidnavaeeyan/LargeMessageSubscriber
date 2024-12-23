using System.Globalization;

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
    }

    public T Result { get; set; }
    public IEnumerable<int> ErrorTypes { get; set; }
    public IEnumerable<int> WarningTypes { get; set; }
    public string Message { get; set; }
  }
}
