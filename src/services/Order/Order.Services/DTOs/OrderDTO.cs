namespace Order.Services.DTOs;
public class OrderDto {
    public OrderDto(long id, string orderStatus, long person)
    {
        Id = id;
        OrderStatus = orderStatus;
        Person = person;
    }
    protected OrderDto () {}
    public long Id {get; set;}
    public string OrderStatus { get; set; }
    public long Person {get; set;}
}