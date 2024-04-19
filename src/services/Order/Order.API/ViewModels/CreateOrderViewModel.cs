namespace Order.Api.ViewModels;

public class CreateOrderViewModel
{
    public required string OrderStatus { get; set; }
    public required long Person {get; set;}
}