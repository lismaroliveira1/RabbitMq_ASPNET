using System.ComponentModel.DataAnnotations;

namespace Client.Domain.Entities;

public abstract class Base {
    [Key]
    public long Id {get; set;}
    public DateTime CreateAt {get; set;}
    public DateTime Id {get; set;}
    internal List<string> _errors;
    public IReadOnlyCollection<string>? Errors => _errors;
    public abstract bool Validate();
}