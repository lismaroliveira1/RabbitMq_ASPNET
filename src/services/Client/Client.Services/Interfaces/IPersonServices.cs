using Client.Services.DTOs;

namespace Client.Services.Interfaces;

public interface IPersonService {
    Task<PersonDto> Create(PersonDto personDto);
    Task<PersonDto> Update(PersonDto personDto);
    Task Remove(long id);
    Task<PersonDto> Get(long id);
    Task<List<PersonDto>> GetAll();
}