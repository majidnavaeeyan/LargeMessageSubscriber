using InfluxDB.Client;
using InfluxDB.Client.Writes;
using LargeMessageSubscriber.Domain.DataModels;
using LargeMessageSubscriber.Domain.Repository;
using LargeMessageSubscriber.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LargeMessageSubscriber.Infrastructure.DataAccess
{
  public class PointRepository : IPointRepository
  {
    private readonly string? _token;
    private readonly string? _address;
    private readonly InfluxDBClient _client;
    private readonly InfluxDbSettings _influxDbSettings;

    public PointRepository(IConfiguration configuration/*IOptions<InfluxDbSettings> optins*/)
    {
      //_token = optins.Value.Token;
      //_address = optins.Value.Address;
      //_token = configuration.GetSection("InfluxDb:Token").Value;

      _client = InfluxDBClientFactory.Create("http://localhost:8086/", "PWl6j0OWPAe6Pew1y_DL9ou7AlHKg7fPNvguMcaUhzgN1QgLUnpNRUoEY7BmmAyVQzv6m2WCPxQjk7ugtziQEQ==");
    }

    //public async Task<IEnumerable<T>> QueryAsync<T>(string query)
    //{
    //  var fluxQuery = await _client.GetQueryApi().QueryAsync<T>(query);
    //  return fluxQuery;
    //}

    public async Task InsertAsync(IEnumerable<Point> model)
    {
      var points = new List<PointData>();
      foreach (var item in model)
      {
        var myPoint = PointData.Measurement("myMeasurment");
        myPoint = myPoint.Field(item.Name, item.Value);
        myPoint = myPoint.Timestamp(item.Timestamp.Value, InfluxDB.Client.Api.Domain.WritePrecision.Ns);

        points.Add(myPoint);
      }


      var writeApi = _client.GetWriteApiAsync();
      await writeApi.WritePointsAsync(points, "Daily Bucket", "d9a201a05434532d");
    }
  }
}