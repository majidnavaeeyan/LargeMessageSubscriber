using System.ComponentModel.DataAnnotations;

namespace LargeMessageSubscriber.Domain
{
  public enum ErrorTypes
  {
    [Display(Description = "ارور شماره 01")]
    MyError = 100,
  }
}
