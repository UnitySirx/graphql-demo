using HotChocolate.Authorization;

namespace WebEmpty.Controller;

[Authorize]
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

