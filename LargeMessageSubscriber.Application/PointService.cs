using LargeMessageSubscriber.Domain;
using LargeMessageSubscriber.Domain.Enums;
using LargeMessageSubscriber.Domain.Mappings;
using LargeMessageSubscriber.Domain.Repository;
using LargeMessageSubscriber.Domain.Services;

namespace LargeMessageSubscriber.Application
{
  public class PointService : IPointService
  {
    private readonly IPointRepository _pointRepository;

    public PointService(IPointRepository pointRepository)
    {
      _pointRepository = pointRepository;
    }

    public async Task InsertAsync(IEnumerable<Domain.DTOs.Point> model)
    {
      var (validationResult, errors, warnings) = InsertValidation(model);
      if (!validationResult)
        throw new ValidationException(errors, warnings);



      var dataModel = model.ToDataModels();
      await _pointRepository.InsertAsync(dataModel);
    }

    private (bool, IEnumerable<int>, IEnumerable<int>) InsertValidation(IEnumerable<Domain.DTOs.Point> model)
    {
      var result = true;
      var errors = new List<int>();
      var warnings = new List<int>();


      foreach (var item in model)
      {
        //Number : 100
        if (item.Timestamp is null || item.Timestamp == DateTime.MinValue || item.Timestamp == DateTime.MaxValue)
          errors.Add((int)ErrorTypes.InvalidValueForTimeStampDataType);

        //Number : 101
        if (item.Value is null /*|| model.Value < 0*/)
          errors.Add((int)ErrorTypes.InValidAmoutForValue);

        //Number : 102
        if (string.IsNullOrWhiteSpace(item.Name))
          errors.Add((int)ErrorTypes.InValidAmoutForName);
      }


      ////////////////////////////////////////
      if (errors.Count > 0)
        result = false;

      return (result, errors, warnings);
      ////////////////////////////////////////
    }
  }
}
