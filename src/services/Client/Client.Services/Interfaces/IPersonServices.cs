using Client.Services.DTOs;

namespace Client.Services.Interfaces;

public interface IPersonService {
    Task<PersonDTO> Create(PersonDTO personDTO);
    Task<PersonDTO> Update(PersonDTO personDTO);
    Task Remove(long id);
    Task<PersonDTO> Get(long id);
    Task<List<PersonDTO>> GetAll();
}