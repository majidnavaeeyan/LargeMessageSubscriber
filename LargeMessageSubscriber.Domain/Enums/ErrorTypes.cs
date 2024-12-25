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

    [Description("مقدار وارد شده برای نام کاربری خالی می باشد")]
    UsernameIsNull = 103,

    [Description("مقدار وارد شده برای گذرواژه خالی می باشد")]
    PasswordIsNull = 104,

    [Description("نام کاربری وارد شده معتبر نمی باشد")]
    UsernameIsNotValidInDatabase = 105,

    [Description("نام صادر کننده توکن معتبر نمی باشد")]
    TokenIssuerNameIsNotValid = 106,

    [Description("تاریخ اعتبار توکن تمام شده است")]
    TokenHasExpired = 107,

    [Description("نام کاربری توکن معتبر نیست")]
    UsernameIsNotValid = 108,

    [Description("توکن وارد شده قابل خواندن نیست")]
    InvalidInputToken = 109,

    [Description("دقت خالی می باشد")]
    PrecisionIsNull = 110,

    [Description("دقت معتبر نمی باشد")]
    PrecisionIsNotValid = 111,

    [Description("مقدار میجرمنت خالی می باشد")]
    MeasurementIsNull = 112,

    [Description("تاریخ شروع خالی می باشد")]
    StartTimeIsNull = 113,

    [Description("تاریخ پایان خالی می باشد")]
    EndTimeIsNull = 114,
  }
}
