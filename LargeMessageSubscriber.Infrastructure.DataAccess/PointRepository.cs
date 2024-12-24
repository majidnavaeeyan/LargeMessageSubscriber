using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using LargeMessageSubscriber.Domain.DataModels;
using LargeMessageSubscriber.Domain.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LargeMessageSubscriber.Infrastructure.DataAccess
{
  public class PointRepository : IPointRepository
  {
    private readonly InfluxDBClient _client;

    public PointRepository()
    {
      var options = new InfluxDBClientOptions.Builder().Url("http://localhost:8086").AuthenticateToken("PWl6j0OWPAe6Pew1y_DL9ou7AlHKg7fPNvguMcaUhzgN1QgLUnpNRUoEY7BmmAyVQzv6m2WCPxQjk7ugtziQEQ==".ToCharArray()).Build();
      _client = InfluxDBClientFactory.Create(options);
      _client.EnableGzip();
    }

    public async Task InsertAsync(IEnumerable<Point> model)
    {
      var points = new List<PointData>();
      foreach (var item in model)
      {
        var myPoint = PointData.Measurement("myMeasurment");
        myPoint = myPoint.Field(item.Name, item.Value);
        myPoint = myPoint.Timestamp(item.Timestamp.GetValueOrDefault(), WritePrecision.Ns);

        points.Add(myPoint);
      }

      Console.WriteLine($"Count : {points.Count} , Time : {DateTime.Now.Second}");

      var writeApi = _client.GetWriteApiAsync();
      var batches = points.Select((value, index) => new { value, index }).GroupBy(x => x.index / 1000).Select(g => g.Select(x => x.value).ToList()).ToList();

      foreach (var item in batches)
      {
        await writeApi.WritePointsAsync(item, "Daily_Bucket", "d9a201a05434532d");
      }
    }
  }
}