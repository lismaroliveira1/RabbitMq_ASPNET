using AutoMapper;
using Client.Domain.Entities;
using Client.Services.DTOs;

namespace Client.Services.Profiles;

public class PersonAutoMapProfile : Profile {
    public PersonAutoMapProfile() {
        CreateMap<PersonDTO, Person>();
    }
}