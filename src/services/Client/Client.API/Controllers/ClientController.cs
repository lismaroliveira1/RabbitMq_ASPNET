using AutoMapper;
using Client.API.Utilities;
using Client.API.ViewModels;
using Client.Core.Exceptions;
using Client.Services.DTOs;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers;


[ApiController]
public class ClientController : ControllerBase {

    private readonly IPersonService _personServices;
    private readonly IMapper _mapper;

    public ClientController(IPersonService personServices, IMapper mapper)
    {
        _personServices = personServices;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("/api/v1/clients/{id}")]
     public async Task<IActionResult> Get(long id)
    {
        try
        {
            var personDto = await _personServices.Get(id);
            if (personDto == null) return StatusCode(404, "User not found");
            return Ok(new ResultViewModel
            {
                Message = "User found successfully",
                Data = new {},
                Success = true
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }
    
    [HttpGet]
    [Route("/api/v1/clients")]
    public async Task<IActionResult> GetAll() {
        try {
            var clients = _personServices.GetAll();
            return Ok(new ResultViewModel
            {
                Message = "Users found successfully",
                Success = true,
                Data = clients
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }

    [HttpPost]
    [Route("/api/v1/clients/create")]
    public async Task<IActionResult> Create([FromBody] PersonCreationViewModel client)
    {
        try
        {
            var userDTO = _mapper.Map<PersonDto>(client);
            var newPerson = await _personServices.Create(userDTO);
            return Ok(new ResultViewModel
            {
                Message = "User created successfully",
                Success = true,
                Data = newPerson
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }

    [HttpPut]
    [Route("/api/v1/clients/update")]
    public async Task<IActionResult> Update([FromBody] PersonUpdatingViewModel client)
    {
        try
        {
            var userDTO = _mapper.Map<PersonDto>(client);
            var user = await _personServices.Update(userDTO);
            return Ok(new ResultViewModel
            {
                Message = "User created successfully",
                Success = true,
                Data = user
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }

    [HttpDelete]
    [Route("api/v1/clients/delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        try {
            await _personServices.Remove(id);
            return Ok(new ResultViewModel{
                Message = "User deleted successfully",
                Success = true,
                Data = new {}
            });
        }catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }
}
