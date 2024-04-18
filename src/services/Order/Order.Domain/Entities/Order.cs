using Order.Domain.Validators;

namespace Order.Domain.Entities;
public class OrderEntity : Base
{

    public long OrderId { get; private set; }
    public string OrderStatus { get; private set; }
    public long Person {get; private set;}
    
    protected OrderEntity() {}

    public OrderEntity(long orderId, string orderStatus, long person)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        Person = person;
    }

    public override bool Validate()
        {
            var validators = new OrderValidator();
            var validationErrors = validators.Validate(this);
            if (!validationErrors.IsValid)
            {
                foreach (var error in validationErrors.Errors)
                {
                    _errors?.Add(error.ErrorMessage);
                }
                throw new Exception("Something is wrong, please fix it and try again.");
            }
            return validationErrors.IsValid;
        }
}