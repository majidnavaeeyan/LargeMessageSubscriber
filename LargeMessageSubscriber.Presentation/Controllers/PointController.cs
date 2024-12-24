using LargeMessageSubscriber.Domain;
using LargeMessageSubscriber.Domain.Mappings;
using LargeMessageSubscriber.Domain.Services;
using LargeMessageSubscriber.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LargeMessageSubscriber.Presentation.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PointController : ControllerBase
  {
    private readonly ILogger<PointController> _logger;
    private readonly IPointService _pointService;

    public PointController(ILogger<PointController> logger, IPointService pointService)
    {
      _logger = logger;
      _pointService = pointService;
    }

    [HttpPost("Insert")]
    public async Task<ApiResult<bool>> InsertAsync(IEnumerable<Point> model)
    {
      try
      {
        var dto = model.ToDTO();
        await _pointService.InsertAsync(dto);
        var result = new ApiResult<bool>(true, new List<int>(), new List<int>());

        return result;
      }
      catch (ValidationException ex)
      {
        var result = new ApiResult<bool>(false, ex.ErrorTypes, ex.WarningTypes);
        return result;
      }
      catch (Exception ex)
      {
        var result = new ApiResult<bool>(false, new List<int>(), new List<int>(), ex.Message);
        return result;
      }
    }
  }
}
