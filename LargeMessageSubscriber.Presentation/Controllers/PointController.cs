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

    [HttpGet("SimulateLargeInsert")]
    public async Task SimulateLargeInsertAsync()
    {
      for (var i = 0; i < 10; i++)
      {
        var data = MakeDate();
        await _pointService.EnqueuMessageToMessageBrokerAsync(data);
      }
    }

    private DateTime RandomDay()
    {
      var gen = new Random();
      var start = new DateTime(2024, 12, 20);
      var range = (DateTime.Today - start).Days;
      return start.AddDays(gen.Next(range));
    }

    private List<Domain.DTOs.Point> MakeDate()
    {
      var myDictionary = new Dictionary<string, Domain.DTOs.Point>();

      for (int i = 0; i < 5000; i++)
      {
        var name = string.Join("", Enumerable.Repeat(0, 5).Select(n => (char)new Random().Next(97, 122)));
        var item = new Domain.DTOs.Point { Name = name, Timestamp = RandomDay(), Value = new Random().Next(1, 100) };
        myDictionary.TryAdd(name, item);
      }

      var model = myDictionary.Select(q => q.Value).ToList();

      return model;
    }
  }
}
