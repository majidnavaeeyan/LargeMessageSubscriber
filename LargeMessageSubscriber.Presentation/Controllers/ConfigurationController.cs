using LargeMessageSubscriber.Domain;
using LargeMessageSubscriber.Domain.Services;
using LargeMessageSubscriber.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LargeMessageSubscriber.Presentation.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ConfigurationController : ControllerBase
  {
    private readonly IConfigurationService _configurationService;

    public ConfigurationController(IConfigurationService configurationService)
    {
      _configurationService = configurationService;
    }

    [HttpGet("GetAllErrors")]
    public ApiResult<IEnumerable<ErrorWarningModel>> GetAllErrorTypes()
    {
      try
      {
        var data = _configurationService.GetAllErrorTypes();
        var result = new ApiResult<IEnumerable<ErrorWarningModel>>(data, new List<int>(), new List<int>());

        return result;
      }
      catch (ValidationException ex)
      {
        var result = new ApiResult<IEnumerable<ErrorWarningModel>>(new List<ErrorWarningModel>(), ex.ErrorTypes, ex.WarningTypes);
        return result;
      }
      catch (Exception ex)
      {
        var result = new ApiResult<IEnumerable<ErrorWarningModel>>(new List<ErrorWarningModel>(), new List<int>(), new List<int>(), ex.Message);
        return result;
      }
    }

    [HttpGet("GetAllWarnings")]
    public ApiResult<IEnumerable<ErrorWarningModel>> GetAllWarningTypes()
    {
      try
      {
        var data = _configurationService.GetAllWarningTypes();
        var result = new ApiResult<IEnumerable<ErrorWarningModel>>(data, new List<int>(), new List<int>());

        return result;
      }
      catch (ValidationException ex)
      {
        var result = new ApiResult<IEnumerable<ErrorWarningModel>>(new List<ErrorWarningModel>(), ex.ErrorTypes, ex.WarningTypes);
        return result;
      }
      catch (Exception ex)
      {
        var result = new ApiResult<IEnumerable<ErrorWarningModel>>(new List<ErrorWarningModel>(), new List<int>(), new List<int>(), ex.Message);
        return result;
      }
    }

    [HttpPost("GenerateJwtToken")]
    public ApiResult<string> GenerateJwtToken([FromBody] TokenInputModel model)
    {
      try
      {
        var data = _configurationService.GenerateJwtToken(model);
        var result = new ApiResult<string>(data, new List<int>(), new List<int>());

        return result;
      }
      catch (ValidationException ex)
      {
        var result = new ApiResult<string>(string.Empty, ex.ErrorTypes, ex.WarningTypes);
        return result;
      }
      catch (Exception ex)
      {
        var result = new ApiResult<string>(string.Empty, new List<int>(), new List<int>(), ex.Message);
        return result;
      }
    }
  }
}