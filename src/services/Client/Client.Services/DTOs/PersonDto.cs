namespace Client.Services.DTOs;

public class PersonDTO {
    public string Name {get; private set;}
    public int Age {get; private set;}
    public string Role {get; private set;}
    public string Document {get; private set;}

    protected PersonDTO() {}

    public PersonDTO(string name, int age, string role, string document)
    {
        Name = name;
        Age = age;
        Role = role;
        Document = document;
    }
}