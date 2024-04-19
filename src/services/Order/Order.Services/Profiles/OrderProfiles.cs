using AutoMapper;
using Order.Domain.Entities;
using Order.Services.DTOs;

namespace Order.Services.Profiles;

public class OrderProfile :Profile {
    public OrderProfile() {
        CreateMap<OrderDto, OrderEntity>();
    }
}