namespace Order.Domain.Entities;

public abstract class Base
{
    public long Id { get; set; }
    public DateTime CreatedAt {get; set;}
    public DateTime UpdateAt {get; set;}
    internal List<string>? _errors;
    public IReadOnlyCollection<string>? Errors => _errors;
    public abstract bool Validate();
}