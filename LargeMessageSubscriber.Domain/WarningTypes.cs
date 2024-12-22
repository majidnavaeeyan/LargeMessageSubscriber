using System.ComponentModel.DataAnnotations;

namespace LargeMessageSubscriber.Domain
{
  public enum WarningTypes
  {
    [Display(Description = "ارور شماره 01")]
    MyError = 100,
  }
}
