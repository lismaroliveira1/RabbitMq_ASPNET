namespace Client.Domain.Entities;

public class Person : Base
{
    public string Name {get; set;}
    public int Age {get; set;}
    public string Role {get; set;}
    public string Document {get; set;}

    public override bool Validate()
    {
        throw new NotImplementedException();
    }

   public static class UserRole 
    {
       public static readonly string Admin = "ADMIN";
       public static readonly string Default = "DEFAULT";
    }
}