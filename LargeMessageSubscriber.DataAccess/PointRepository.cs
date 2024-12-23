using InfluxDB.Client;
using InfluxDB.Client.Writes;
using LargeMessageSubscriber.Domain.DataModels;
using LargeMessageSubscriber.Domain.Repository;
using Microsoft.Extensions.Configuration;

namespace LargeMessageSubscriber.Infrastructure.DataAccess
{
  public class PointRepository : IPointRepository
  {
    private readonly string? _token;
    private readonly string? _address;
    private readonly InfluxDBClient _client;

    public PointRepository(IConfiguration configuration)
    {
      _token = configuration.GetSection("InfluxDB:Token").Value;
      _address = configuration.GetSection("InfluxDB:Address").Value;
      _client = InfluxDBClientFactory.Create(_address, _token);
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