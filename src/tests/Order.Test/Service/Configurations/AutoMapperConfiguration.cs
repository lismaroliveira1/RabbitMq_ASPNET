using AutoMapper;
using Order.Domain.Entities;
using Order.Services.DTOs;

namespace Order.Test.Service.Configurations;

public static class AutoMapperConfiguration
{
    public static IMapper GetConfig()
    {
        var autoMapperConfig = new MapperConfiguration(
            config => {
                config.CreateMap<OrderDto, OrderEntity>();
                config.CreateMap<OrderEntity, OrderDto>();
                }
            );
        return autoMapperConfig.CreateMapper();
    }
}