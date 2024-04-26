using AutoMapper;
using Client.Domain.Entity;
using Client.Services.DTOs;

namespace Client.test.Service.Configurations;

public static class AutoMapperConfiguration
{
    public static IMapper GetConfig()
    {
        var autoMapperConfig = new MapperConfiguration(
            config => {
                config.CreateMap<PersonDto, PersonEntity>();
                config.CreateMap<PersonEntity, PersonDto>();
                }
            );
        return autoMapperConfig.CreateMapper();
    }
}