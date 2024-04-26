using AutoMapper;
using Bogus;
using FluentAssertions;
using MessageBroker.Core.Model;
using MessageBroker.EventBus.Interfaces;
using Moq;
using Order.Core.Exceptions;
using Order.Domain.Entities;
using Order.Infra.Interfaces;
using Order.Services.Bundles;
using Order.Services.DTOs;
using Order.Services.Interfaces;
using Order.Test.Service.Configurations;
using Xunit;

namespace Order.Test.Service;

public class ServiceTest
{
    private readonly IOrderService _sut;
    private readonly IMapper _mapper;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    private readonly Mock<IMbClient> _mBClientMock;
    Bogus.Faker<OrderDto> orderDtoBogus = new Bogus.Faker<OrderDto>();
    public ServiceTest()
    {
        _mapper = AutoMapperConfiguration.GetConfig();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _mBClientMock = new Mock<IMbClient>();
        _sut = new OrderService(
            mapper: _mapper,
            orderRepository: _orderRepositoryMock.Object,
            mbClient: _mBClientMock.Object
            );

         orderDtoBogus.CustomInstantiator(faker => new OrderDto
        {
            Id = faker.UniqueIndex, 
            OrderStatus = "Canceled",
            Person = 1
        });
    }
    [Fact(DisplayName = "Create Valid Order")]
    public async void ShouldCreateAValidOrderAndReturnsAValidData() {
        //Arranges
        var orderDto = orderDtoBogus.Generate();
        var Order = _mapper.Map<OrderEntity>(orderDto);
        _orderRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => null);
        _orderRepositoryMock.Setup(x => x.Create(It.IsAny<OrderEntity>())).ReturnsAsync(() => Order);
        _mBClientMock.Setup(x => x.Call<Request>(It.IsAny<Request>())).Returns(() => new Response());

        //Act
        var result = await _sut.Create(orderDto);
        
        //Assert
        result.Should().BeEquivalentTo(orderDto);
    }

    [Fact(DisplayName = "Order creating exception ")]
    public void ShouldNotCreateAValidDTOUserAndReturnsAValidData() {
        //Arranges
        var orderDto = orderDtoBogus.Generate();
        var order = _mapper.Map<OrderEntity>(orderDto);
        _orderRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => order);
        _orderRepositoryMock.Setup(x => x.Create(It.IsAny<OrderEntity>())).ReturnsAsync(() => order);

        //Act
         Func<Task<OrderDto>> act = async() => await _sut.Create(orderDto);
        
        //Assert
        act.Should().ThrowAsync<DomainException>();
    }

    [Fact(DisplayName = "Order Get by Id")]
    public async Task ShouldProvideAValidUserIfValidUserId() {
        //Arranges
        var userId = new Randomizer().Int(1, 9999999);
        var OrderDto = orderDtoBogus.Generate();
        var Order = _mapper.Map<OrderEntity>(OrderDto);
        _orderRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => Order);
        //Act
        var result = await _sut.Get(userId);
        
        //Assert
        result.Should().BeEquivalentTo(OrderDto);
    }

    [Fact(DisplayName = "Order get all orders")]
    public async void ShouldReturnsAllUsers()
    {
        //Arranges
        var orderDto = orderDtoBogus.Generate();
        var order = _mapper.Map<OrderEntity>(orderDto);
        var orders = new List<OrderEntity>();
        orders.Add(order);
       _orderRepositoryMock.Setup(x => x.GetAll()).Returns(async () => orders);

        //Act
        var result = await _sut.GetAll();

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<List<OrderDto>>(orders));
    }


    [Fact(DisplayName = "Update Order")]
    public async void ShouldUpdateUserWithAValidData()
    {
        //Arranges
        var orderDto = orderDtoBogus.Generate();
        var order = _mapper.Map<OrderEntity>(orderDto);
        _orderRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => order);
        _orderRepositoryMock.Setup(x => x.Update(It.IsAny<OrderEntity>())).ReturnsAsync(() => order);

        //Act
        var result = await _sut.Update(orderDto);

        //Assert
        result.Should().BeEquivalentTo(orderDto);
    }

    [Fact(DisplayName = "Remove Order")]
    public async void ShouldRemoveUserWithAValidData()
    {
        //Arranges
        var userId = new Randomizer().Long(1, 9999999);
        _orderRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Verifiable();


        //Act
        await _sut.Remove(userId);
    
        //Assert
        _orderRepositoryMock.Verify(x => x.Delete(userId), Times.Once);
    }
}
