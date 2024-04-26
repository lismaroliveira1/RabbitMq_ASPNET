using AutoMapper;
using Bogus;
using FluentAssertions;
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
    private readonly Mock<IOrderRepository> _orderRepository;
    Bogus.Faker<OrderDto> orderDtoBogus = new Bogus.Faker<OrderDto>();
    public ServiceTest()
    {
        _mapper = AutoMapperConfiguration.GetConfig();
        _orderRepository = new Mock<IOrderRepository>();

        _sut = new OrderService(
            mapper: _mapper,
            orderRepository: _orderRepository.Object
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
        _orderRepository.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => null);
        _orderRepository.Setup(x => x.Create(It.IsAny<OrderEntity>())).ReturnsAsync(() => Order);

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
        _orderRepository.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => order);
        _orderRepository.Setup(x => x.Create(It.IsAny<OrderEntity>())).ReturnsAsync(() => order);

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
        _orderRepository.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => Order);
        //Act
        var result = await _sut.Get(userId);
        
        //Assert
        result.Should().BeEquivalentTo(OrderDto);
    }

    [Fact(DisplayName = "User get all orders")]
    public async void ShouldReturnsAllUsers()
    {
        //Arranges
        var orderDto = orderDtoBogus.Generate();
        var order = _mapper.Map<OrderEntity>(orderDto);
        var orders = new List<OrderEntity>();
        orders.Add(order);
       _orderRepository.Setup(x => x.GetAll()).Returns(async () => orders);

        //Act
        var result = await _sut.GetAll();

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<List<OrderDto>>(orders));
    }

}
