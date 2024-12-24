using LargeMessageSubscriber.Domain.Enums;
using LargeMessageSubscriber.Domain.Services;
using LargeMessageSubscriber.Domain.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LargeMessageSubscriber.Application
{
  public class ConfigurationService : IConfigurationService
  {
    public IEnumerable<ErrorWarningModel> GetAllErrorTypes()
    {
      var result = new List<ErrorWarningModel>();

      var items = Enum.GetValues(typeof(ErrorTypes));

      foreach (var item in items)
      {
        var data = new ErrorWarningModel { EnglishValue = item.ToString(), Id = item.GetHashCode(), PersianValue = ((DescriptionAttribute)item?.GetType()?.GetMember(item.ToString())?.First()?.GetCustomAttribute(typeof(DescriptionAttribute), false)).Description };
        result.Add(data);
      }

      return result;
    }

    public IEnumerable<ErrorWarningModel> GetAllWarningTypes()
    {
      var result = new List<ErrorWarningModel>();

      var items = Enum.GetValues(typeof(WarningTypes));

      foreach (var item in items)
      {
        var data = new ErrorWarningModel { EnglishValue = item.ToString(), Id = item.GetHashCode(), PersianValue = ((DescriptionAttribute)item.GetType().GetMember(item.ToString()).First().GetCustomAttribute(typeof(DescriptionAttribute), false)).Description };
        result.Add(data);
      }

      return result;
    }
  }
}