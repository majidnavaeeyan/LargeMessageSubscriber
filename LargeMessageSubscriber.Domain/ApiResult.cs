namespace LargeMessageSubscriber.Domain
{
  public sealed class ApiResult<T>
  {
    public ApiResult(T result, int errorTypes, int warningTypes, string message)
    {
      Result = result;
      ErrorTypes = errorTypes;
      WarningTypes = warningTypes;
      Message = message;
    }

    public T Result { get; set; }
    public int ErrorTypes { get; set; }
    public int WarningTypes { get; set; }
    public string Message { get; set; }
  }
}
