namespace Order.API.ViewModels;
public class UpdateOrderViewModel
{
    public long Id { get; set; }
    public required string OrderStatus { get; set; }
    public long Person { get; set; }
}