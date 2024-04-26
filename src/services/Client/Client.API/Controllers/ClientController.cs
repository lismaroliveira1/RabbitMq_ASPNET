using AutoMapper;
using Client.API.Utilities;
using Client.API.ViewModels;
using Client.Core.Exceptions;
using Client.Services.DTOs;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers;


[ApiController]
public class ClientController : ControllerBase
{

    private readonly IPersonService _personServices;
    private readonly IMapper _mapper;

    private readonly ILoggerService _logger;

    public ClientController(IPersonService personServices, IMapper mapper, ILoggerService logger)
    {
        _personServices = personServices;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Route("/api/v1/clients/{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            _logger.LogInfo("Starting request ...");
            var personDto = await _personServices.Get(id);
            if (personDto == null) return StatusCode(404, "User not found");
            _logger.LogInfo("Request completed successfully");
            return Ok(new ResultViewModel
            {
                Message = "User found successfully",
                Data = personDto,
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
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInfo("Starting request ...");
            var clients = _personServices.GetAll();
            _logger.LogInfo("Request completed successfully");
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
            _logger.LogInfo("Starting request ...");
            var userDTO = _mapper.Map<PersonDto>(client);
            var newPerson = await _personServices.Create(userDTO);
            _logger.LogInfo("Request completed successfully");
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
            _logger.LogInfo("Starting request ...");
            var userDTO = _mapper.Map<PersonDto>(client);
            var user = await _personServices.Update(userDTO);
            _logger.LogInfo("Request completed successfully");
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
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            _logger.LogInfo("Starting request ...");
            await _personServices.Remove(id);
            _logger.LogInfo("Request completed successfully");
            return Ok(new ResultViewModel
            {
                Message = "User deleted successfully",
                Success = true,
                Data = new { }
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
}
