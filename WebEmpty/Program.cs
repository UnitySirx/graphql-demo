var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(new InMemoryData());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    ;
var app = builder.Build();
app.MapGraphQL();
app.Run();


public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UpdatePersonInput
{
    public string Name { get; set; }
}

public class PersonInput
{
    public string Name { get; set; }
}

public class InMemoryData
{
    public List<Person> People { get; } = new List<Person>();

    public void AddPerson(Person person)
    {
        People.Add(person);
    }

    public Person GetPersonById(string id)
    {
        return People.FirstOrDefault(p => p.Id.Equals(id));
    }

    public void UpdatePerson(string id, Person person)
    {
        var index = People.FindIndex(p => p.Id.Equals(id));
        if (index != -1)
        {
            People[index] = person;
        }
    }

    public void DeletePerson(string id)
    {
        People.RemoveAll(p => p.Id.Equals(id));
    }
}

public class Query  
{  
    private readonly InMemoryData _data;  
  
    public Query(InMemoryData data)  
    {        _data = data;  
    }  
    public IQueryable<Person> GetPeople() => _data.People.AsQueryable();  
  
    public Person GetPerson(string id) => _data.GetPersonById(id);  
}  
  
public class Mutation  
{  
    private readonly InMemoryData _data;  
  
    public Mutation(InMemoryData data)  
    {        _data = data;  
    }  
    public Person CreatePerson(PersonInput input)  
    {        var person = new Person  
        {  
            Id = Guid.NewGuid().ToString("N"),   
            Name = input.Name  
        };  
        _data.AddPerson(person);  
        return person;  
    }  
    public Person UpdatePerson(string id, UpdatePersonInput input)  
    {        var person = _data.GetPersonById(id);  
        if (person != null)  
        {            person.Name = input.Name;  
            return person;  
        }        throw new Exception("Person not found");  
    }  
    public bool DeletePerson(string id)  
    {        var success = _data.People.RemoveAll(p => p.Id.Equals(id)) > 0;  
        return success;  
    }}