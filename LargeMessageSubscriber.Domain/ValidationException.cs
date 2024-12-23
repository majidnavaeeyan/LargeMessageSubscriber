namespace LargeMessageSubscriber.Domain
{
  public class ValidationException : Exception
  {
    public IEnumerable<int> ErrorTypes { get; set; }
    public IEnumerable<int> WarningTypes { get; set; }

    public ValidationException(IEnumerable<int> errorTypes, IEnumerable<int> warningTypes)
    {
      ErrorTypes = errorTypes;
      WarningTypes = warningTypes;
    }
  }
}
