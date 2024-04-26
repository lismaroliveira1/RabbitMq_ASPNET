using Client.Domain.Validators;

namespace Client.Domain.Entity;

public class PersonEntity : Base
{
    public string Name {get; private set;}
    public int Age {get; private set;}
    public string Role {get; private set;}
    public string Document {get; private set;}

    protected PersonEntity() {}

    public PersonEntity(string name, int age, string role, string document)
    {
        Name = name;
        Age = age;
        Role = role;
        Document = document;
    }

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

}