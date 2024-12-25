using LargeMessageSubscriber.Domain;
using LargeMessageSubscriber.Domain.Enums;
using LargeMessageSubscriber.Domain.Mappings;
using LargeMessageSubscriber.Domain.MessageBroker;
using LargeMessageSubscriber.Domain.Repository;
using LargeMessageSubscriber.Domain.Services;
using LargeMessageSubscriber.Domain.ViewModels;

namespace LargeMessageSubscriber.Application
{
  public class PointService : IPointService
  {
    private readonly IPointRepository _pointRepository;
    private readonly IMessageConsumer _messageConsumer;
    private readonly IMessageProducer _messageProducer;

    public PointService(IPointRepository pointRepository, IMessageConsumer messageConsumer, IMessageProducer messageProducer)
    {
      _pointRepository = pointRepository;
      _messageConsumer = messageConsumer;
      _messageProducer = messageProducer;
    }

    public async Task InsertAsync(IEnumerable<Domain.DTOs.Point> model)
    {
      var (validationResult, errors, warnings) = InsertValidation(model);
      if (!validationResult)
        throw new ValidationException(errors, warnings);



      var dataModel = model.ToDataModels();
      await _pointRepository.InsertAsync(dataModel);
    }

    public async Task<IEnumerable<QueryResult>> GetAsync(QueryModel model)
    {
      var (validationResult, errors, warnings) = GetValidation(model);
      if (!validationResult)
        throw new ValidationException(errors, warnings);



      var data = await _pointRepository.GetAsync(model);
      return data;
    }

    public async Task InsertRecievedMessagesToDbAsync()
    {
      //some validation can be done here

      var data = await _messageConsumer.ConsumeMessageAsync();

      var dataModels = data.ToDataModels();

      if (dataModels.Count() > 0)
        await _pointRepository.InsertAsync(dataModels);
    }

    public async Task EnqueuMessageToMessageBrokerAsync(List<Domain.DTOs.Point> model)
    {
      await _messageProducer.ProduceMessageAsync(model);
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

    private (bool, IEnumerable<int>, IEnumerable<int>) GetValidation(QueryModel model)
    {
      var result = true;
      var errors = new List<int>();
      var warnings = new List<int>();



      //Number : 110
      if (string.IsNullOrWhiteSpace(model.Precision))
        errors.Add((int)ErrorTypes.PrecisionIsNull);

      //Number : 111
      var validPrecision = new List<string> { "hourly", "daily" };
      if (!validPrecision.Contains(model.Precision))
        errors.Add((int)ErrorTypes.PrecisionIsNotValid);

      //Number : 112
      if (string.IsNullOrWhiteSpace(model.MeasurementName))
        errors.Add((int)ErrorTypes.MeasurementIsNull);

      //Number : 113
      if (model.StartTime is null || model.StartTime == DateTime.MinValue || model.StartTime == DateTime.MaxValue)
        errors.Add((int)ErrorTypes.StartTimeIsNull);

      //Number : 114
      if (model.EndTime is null || model.EndTime == DateTime.MinValue || model.EndTime == DateTime.MaxValue)
        errors.Add((int)ErrorTypes.EndTimeIsNull);



      ////////////////////////////////////////
      if (errors.Count > 0)
        result = false;

      return (result, errors, warnings);
      ////////////////////////////////////////
    }
  }
}
