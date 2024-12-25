using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using LargeMessageSubscriber.Domain.Repository;
using LargeMessageSubscriber.Domain.ViewModels;

namespace LargeMessageSubscriber.Infrastructure.DataAccess
{
  public class PointRepository : IPointRepository
  {
    private readonly InfluxDBClient _client;

    public PointRepository()
    {
      var options = new InfluxDBClientOptions.Builder().Url("http://localhost:8086").AuthenticateToken("PWl6j0OWPAe6Pew1y_DL9ou7AlHKg7fPNvguMcaUhzgN1QgLUnpNRUoEY7BmmAyVQzv6m2WCPxQjk7ugtziQEQ==".ToCharArray()).Build();
      _client = InfluxDBClientFactory.Create(options);
    }

    public async Task InsertAsync(IEnumerable<Domain.DataModels.Point> model)
    {
      var points = new List<PointData>();
      foreach (var item in model)
      {
        var myPoint = PointData.Measurement("myMeasurment");
        myPoint = myPoint.Field(item.Name, item.Value);
        myPoint = myPoint.Timestamp(item.Timestamp.GetValueOrDefault(), WritePrecision.Ns);

        points.Add(myPoint);
      }

      Console.WriteLine($"Count : {points.Count} , Time : {DateTime.Now.Second} , DateTime : {DateTime.Now.Second}");

      var writeOptions = WriteOptions.CreateNew().BatchSize(5000).FlushInterval(1000).Build();
      var writeApi = _client.GetWriteApi(writeOptions);
      var batches = points.Select((value, index) => new { value, index }).GroupBy(x => x.index / 1000).Select(g => g.Select(x => x.value).ToList()).ToList();

      writeApi.WritePoints(points, "Daily_Bucket", "d9a201a05434532d");
    }

    public async Task<IEnumerable<QueryResult>> GetAsync(QueryModel model)
    {
      var fluxQuery = BuildQuery(model);

      var queryApi = _client.GetQueryApi();
      var tables = await queryApi.QueryAsync(fluxQuery, "d9a201a05434532d");

      var result = tables.SelectMany(table => table.Records.Select(record => new QueryResult { Time = record.GetTime()?.ToDateTimeUtc(), Value = record.GetValue(), Field = record.GetField(), Measurement = record.GetMeasurement() }));

      return result;
    }

    private string BuildQuery(QueryModel model)
    {
      var timePrecision = model.Precision.ToLower() switch { "hourly" => "1h", "daily" => "1d", _ => "raw" };

      var bucketFilter = $"from(bucket: \"Daily_Bucket\")";
      var rangeFilter = $"|> range(start: {model.StartTime:yyyy-MM-ddTHH:mm:ssZ}, stop: {model.EndTime:yyyy-MM-ddTHH:mm:ssZ})";
      var measurementFilter = $"|> filter(fn: (r) => r._measurement == \"{model.MeasurementName}\")";
      var fieldFilter = string.IsNullOrWhiteSpace(model.FieldName) ? string.Empty : $"|> filter(fn: (r) => r._field == \"{model.FieldName}\")";

      // Add aggregation based on precision
      var precisionQuery = model.Precision.ToLower() switch
      {
        "hourly" or "daily" => $"|> aggregateWindow(every: {timePrecision}, fn: mean, createEmpty: false)",
        _ => ""
      };

      return $"{bucketFilter} {rangeFilter} {measurementFilter} {fieldFilter} {precisionQuery}";
    }
  }
}