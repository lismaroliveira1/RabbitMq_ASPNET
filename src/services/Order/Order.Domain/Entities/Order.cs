namespace Order.Domain.Entities;
public class Order : Base
{

    public long OrderId { get; private set; }
    public string OrderStatus { get; private set; }
    public long Person {get; private set;}
    
    protected Order() {}

    public Order(long orderId, string orderStatus, long person)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        Person = person;
    }

    public override bool Validate()
    {
        throw new NotImplementedException();
    }
}