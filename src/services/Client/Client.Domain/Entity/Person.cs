using Client.Domain.Validators;

namespace Client.Domain.Entities;

public class Person : Base
{
    public string Name {get; set;}
    public int Age {get; set;}
    public string Role {get; set;}
    public string Document {get; set;}

    public override bool Validate()
    {
        var validators = new PersonValidator();
            var validation = validators.Validate(this);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors?.Add(error.ErrorMessage);
                }
                throw new Exception("Some errors are wrongs, please fix it and try again.");
            }
            return validation.IsValid;
    }

   public static class UserRole 
    {
       public static readonly string Admin = "ADMIN";
       public static readonly string Default = "DEFAULT";
    }
}