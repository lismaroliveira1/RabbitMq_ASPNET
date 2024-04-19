using AutoMapper;
using Client.Core.Exceptions;
using Client.Domain.Entity;
using Client.Services.DTOs;
using Client.Services.Interfaces;

namespace Client.Services.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonService _personRepository;
    public PersonService(IMapper mapper, IPersonService personService)
    {
        _mapper = mapper;
        _personRepository = personService;
    }

    async Task<PersonDto> IPersonService.Create(PersonDto personDTO)
    {
        var _hasPerson = await _personRepository.Get(personDTO.Id);
        if (_hasPerson != null) throw new DomainException("This person is already registered");
        var person = _mapper.Map<Person>(personDTO);
        person.Validate();
        var newPerson = await _personRepository.Create(personDTO);
        return _mapper.Map<PersonDto>(newPerson);
    }

    async Task<PersonDto> IPersonService.Get(long id)
    {
        var personDTO = await _personRepository.Get(id);
        return _mapper.Map<PersonDto>(personDTO);
    }

    async Task<List<PersonDto>> IPersonService.GetAll()
    {
        var personDTO = await _personRepository.GetAll();
        return _mapper.Map<List<PersonDto>>(personDTO);
    }

    async Task IPersonService.Remove(long id)
    {
        await _personRepository.Remove(id);
    }

    async Task<PersonDto> IPersonService.Update(PersonDto personDTO)
    {
        var _hasPerson = await _personRepository.Get(personDTO.Id);
        if (_hasPerson == null) throw new DomainException("Vehicle not found");
        var vehicle = _mapper.Map<Person>(personDTO);
        vehicle.Validate();
        var newVehicle = await _personRepository.Update(personDTO);
        return _mapper.Map<PersonDto>(newVehicle);
    }
}
