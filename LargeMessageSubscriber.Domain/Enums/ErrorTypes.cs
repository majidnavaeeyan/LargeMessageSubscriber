using System.ComponentModel;

namespace LargeMessageSubscriber.Domain.Enums
{
  public enum ErrorTypes
  {
    [Description("برای داده از نوع سری زمانی مقدار غیر قابل قبولی فرستاده شده است")]
    InvalidValueForTimeStampDataType = 100,

    [Description("برای فیلد ولیو مقدار غیر معتبر وارد شده است")]
    InValidAmoutForValue = 101,

    [Description("برای فیلد نام مقدار غیر معتبر وارد شده است")]
    InValidAmoutForName = 102,
  }
}
