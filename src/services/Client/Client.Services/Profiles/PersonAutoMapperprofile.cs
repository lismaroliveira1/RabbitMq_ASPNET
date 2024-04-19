using AutoMapper;
using Client.Domain.Entity;
using Client.Services.DTOs;

namespace Client.Services.Profiles;

public class PersonAutoMapProfile : Profile {
    public PersonAutoMapProfile() {
        CreateMap<PersonDto, Person>();
    }
}