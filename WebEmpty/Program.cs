using HotChocolate.Subscriptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IPeopleCountService, PeopleCountService>();
builder.Services.AddSingleton(new InMemoryData());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UploadType>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    ;
var app = builder.Build();
app.MapGraphQL();
app.UseWebSockets();//需要使用这个
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

    public void AddPersonRange(List<Person> persons)
    {
        People.AddRange(persons);
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

public class UploadFile
{
   public IFile File { get; set; }
}

public class Query
{
    private readonly InMemoryData _data;

    public Query(InMemoryData data)
    {
        _data = data;
    }

    public IQueryable<Person> GetPeople() => _data.People.AsQueryable();

    public Person GetPerson(string id) => _data.GetPersonById(id);
}

public class Mutation
{
    private readonly InMemoryData _data;
    private readonly IPeopleCountService _peopleCountService;

    public Mutation(InMemoryData data,IPeopleCountService peopleCountService)
    {
        _data = data;
        _peopleCountService = peopleCountService;
    }

    public async Task<Person> CreatePerson(PersonInput input)
    {
        var person = new Person
        {
            Id = Guid.NewGuid().ToString("N"),
            Name = input.Name
        };
        _data.AddPerson(person);
        await _peopleCountService.UpdateDeviceAsync(_data.People.Count);
        return person;
    }

    public List<Person> CreatePersonRange(List<PersonInput> inputs)
    {
        List<Person> persons = new List<Person>();
        foreach (var item in inputs)
        {
            var person = new Person
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = item.Name
            };
            persons.Add(person);
        }

        _data.AddPersonRange(persons);
        return persons;
    }

    public Person UpdatePerson(string id, UpdatePersonInput input)
    {
        var person = _data.GetPersonById(id);
        if (person != null)
        {
            person.Name = input.Name;
            return person;
        }

        throw new Exception("Person not found");
    }

    public async Task<bool> DeletePerson(string id)
    {
        var success = _data.People.RemoveAll(p => p.Id.Equals(id)) > 0;
        await _peopleCountService.UpdateDeviceAsync(_data.People.Count);
        return success;
    }
    
    public async Task<string> UploadFile(UploadFile input,CancellationToken cancellationToken)
    {
        if (Directory.Exists("Uploads") == false)
        {
            Directory.CreateDirectory("Uploads");
        }
        var filePath = System.IO.Path.Combine("Uploads", input.File.Name);
        await using var stream = File.Create(filePath);
        await input.File.CopyToAsync(stream,cancellationToken);
        return filePath;
    }
}


public interface IPeopleCountService
{
    Task UpdateDeviceAsync(int count);
}
public class PeopleCountService:IPeopleCountService
{
    private readonly ITopicEventSender _eventSender;

    public PeopleCountService(ITopicEventSender eventSender)
    {
        _eventSender = eventSender;
    }
    
    public async Task UpdateDeviceAsync(int count)
    {
        // 这里你可以发布事件
        await _eventSender.SendAsync(nameof(Subscription.PeopleCount), count);
        await Console.Out.WriteLineAsync($"数量:{count}");
    }
}
public class Subscription
{
    [Subscribe]
    [Topic]
    public int PeopleCount([EventMessage]int count)
    {
        return count;
    }
}