using Order.Core.Exceptions;
using Order.Domain.Validators;

namespace Order.Domain.Entities;
public class OrderEntity : Base
{

    public string OrderStatus { get; private set; }
    public long Person {get; private set;}
    
    protected OrderEntity() {}

    public OrderEntity(long orderId, string orderStatus, long person)
    {
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
                throw new DomainException("Something is wrong, please fix it and try again.");
            }
            return validationErrors.IsValid;
        }
}