using AutoMapper;
using Client.Domain.Entities;
using Client.Services.DTOs;
using Client.Services.Interfaces;
using Motorcycle.Core.Exceptions;

namespace Client.Service.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonService _personRepository;
    public PersonService(IMapper mapper, IPersonService personService)
    {
        _mapper = mapper;
        _personRepository = personService;
    }

    async Task<PersonDTO> IPersonService.Create(PersonDTO personDTO)
    {
        var _hasPerson = await _personRepository.Get(personDTO.Id);
        if (_hasPerson != null) throw new DomainException("This person is already registered");
        var person = _mapper.Map<Person>(personDTO);
        person.Validate();
        var newPerson = await _personRepository.Create(personDTO);
        return _mapper.Map<PersonDTO>(newPerson);
    }

    async Task<PersonDTO> IPersonService.Get(long id)
    {
        var personDTO = await _personRepository.Get(id);
        return _mapper.Map<PersonDTO>(personDTO);
    }

    async Task<List<PersonDTO>> IPersonService.GetAll()
    {
        var personDTO = await _personRepository.GetAll();
        return _mapper.Map<List<PersonDTO>>(personDTO);
    }

    async Task IPersonService.Remove(long id)
    {
        await _personRepository.Remove(id);
    }

    async Task<PersonDTO> IPersonService.Update(PersonDTO personDTO)
    {
        var _hasPerson = await _personRepository.Get(personDTO.Id);
        if (_hasPerson == null) throw new DomainException("Vehicle not found");
        var vehicle = _mapper.Map<Person>(personDTO);
        vehicle.Validate();
        var newVehicle = await _personRepository.Update(personDTO);
        return _mapper.Map<PersonDTO>(newVehicle);
    }
}
