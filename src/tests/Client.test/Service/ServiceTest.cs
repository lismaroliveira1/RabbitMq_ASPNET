using AutoMapper;
using Bogus.Extensions.Brazil;
using Client.Core.Exceptions;
using Client.Infrastructure.Interfaces;
using Client.Services.DTOs;
using Client.Services.Interfaces;
using Client.Services.Services;
using Client.test.Service.Configurations;
using Client.Domain.Entity;
using MessageBroker.EventBus.Interfaces;
using Moq;
using Bogus;
using FluentAssertions;
using static Client.Domain.Entity.PersonEntity;

namespace Client.test.Service;

public class ServiceTest
{
    private readonly IPersonService _sut;
    private readonly IMapper _mapper;
    private readonly Mock<IPersonRepository> _personRepositoryMock;

    Faker<PersonDto> personDtoBogus = new Faker<PersonDto>();
    public ServiceTest()
    {
        _mapper = AutoMapperConfiguration.GetConfig();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _sut = new PersonService(
            mapper: _mapper,
            personRepository: _personRepositoryMock.Object
            );

         personDtoBogus.CustomInstantiator(faker => new PersonDto
        {
            Id = new Randomizer().Long(1, 9999999),
            Name = faker.Person.FirstName,
            Age = int.Parse(new Randomizer().Long(1, 100).ToString()),
            Role = "ADMIN",
            Document = faker.Person.Cpf().ToString()
        });
    }
    [Fact(DisplayName = "Create Valid User")]
    public async void ShouldCreateAValidUserAndReturnsAValidData() {
        //Arranges
        var userDto = personDtoBogus.Generate();
        var user = _mapper.Map<PersonEntity>(userDto);
        _personRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => null);
        _personRepositoryMock.Setup(x => x.Create(It.IsAny<PersonEntity>())).ReturnsAsync(() => user);

        //Act
        var result = await _sut.Create(userDto);
        
        //Assert
        result.Should().BeEquivalentTo(userDto);
    }

    [Fact(DisplayName = "user creating exception ")]
    public void ShouldNotCreateAValidDTOUserAndReturnsAValidData() {
        //Arranges
        var userDto = personDtoBogus.Generate();
        var user = _mapper.Map<PersonEntity>(userDto);
        _personRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => user);
        _personRepositoryMock.Setup(x => x.Create(It.IsAny<PersonEntity>())).ReturnsAsync(() => user);

        //Act
         Func<Task<PersonDto>> act = async() => await _sut.Create(userDto);
        
        //Assert
        act.Should().ThrowAsync<DomainException>();
    }

    [Fact(DisplayName = "user Get by Id")]
    public async Task ShouldProvideAValidUserIfValidUserId() {
        //Arranges
        var userId = new Randomizer().Int(1, 9999999);
        var userDto = personDtoBogus.Generate();
        var user = _mapper.Map<PersonEntity>(userDto);
        _personRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => user);
        //Act
        var result = await _sut.Get(userId);
        
        //Assert
        result.Should().BeEquivalentTo(userDto);
    }

    [Fact(DisplayName = "user get all users")]
    public async void ShouldReturnsAllUsers()
    {
        //Arranges
        var userDto = personDtoBogus.Generate();
        var user = _mapper.Map<PersonEntity>(userDto);
        var users = new List<PersonEntity>();
        users.Add(user);
       _personRepositoryMock.Setup(x => x.GetAll()).Returns(async () => users);

        //Act
        var result = await _sut.GetAll();

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<List<PersonDto>>(users));
    }


    [Fact(DisplayName = "Update user")]
    public async void ShouldUpdateUserWithAValidData()
    {
        //Arranges
        var userDto = personDtoBogus.Generate();
        var user = _mapper.Map<PersonEntity>(userDto);
        _personRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => user);
        _personRepositoryMock.Setup(x => x.Update(It.IsAny<PersonEntity>())).ReturnsAsync(() => user);

        //Act
        var result = await _sut.Update(userDto);

        //Assert
        result.Should().BeEquivalentTo(userDto);
    }

    [Fact(DisplayName = "Remove user")]
    public async void ShouldRemoveUserWithAValidData()
    {
        //Arranges
        var userId = new Randomizer().Long(1, 9999999);
        _personRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Verifiable();


        //Act
        await _sut.Remove(userId);
    
        //Assert
        _personRepositoryMock.Verify(x => x.Delete(userId), Times.Once);
    }
}
