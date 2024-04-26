namespace Client.Services.DTOs;

public class PersonDto {
    public long Id {get; set;}
    public string? Name {get; set;}
    public int Age {get; set;}
    public string? Role {get; set;}
    public string? Document {get; set;}

    public PersonDto() {}

    protected PersonDto(string? name, int age, string? role, string? document)
    {
        Name = name;
        Age = age;
        Role = role;
        Document = document;
    }
}