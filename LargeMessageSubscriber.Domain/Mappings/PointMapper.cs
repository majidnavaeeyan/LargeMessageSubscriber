using AutoMapper;

namespace LargeMessageSubscriber.Domain.Mappings
{
  public static class PointMapper
  {
    private static IMapper _mapper;

    static PointMapper()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<ViewModels.Point, DTOs.Point>();
        cfg.CreateMap<DTOs.Point, DataModels.Point>();
      });

      _mapper = config.CreateMapper();
    }

    public static IEnumerable<DTOs.Point> ToDTO(this IEnumerable<ViewModels.Point> model)
    {
      return _mapper.Map<IEnumerable<DTOs.Point>>(model);
    }

    public static IEnumerable<DataModels.Point> ToDataModels(this IEnumerable<DTOs.Point> model)
    {
      return _mapper.Map<IEnumerable<DataModels.Point>>(model);
    }
  }
}