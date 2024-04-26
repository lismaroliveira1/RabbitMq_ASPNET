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
                config.CreateMap<PersonDto, Person>();
                config.CreateMap<Person, PersonDto>();
                }
            );
        return autoMapperConfig.CreateMapper();
    }
}