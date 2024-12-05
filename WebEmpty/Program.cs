using Microsoft.Extensions.FileProviders;
using water_api.Services;
using WebEmpty;
using WebEmpty.Controller;
using WebEmpty.jwt;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                ;
        });
});

builder.Services.AddSingleton<IJwtOptionIOC, JwtOptionIOC>();
builder.Services.AddSingleton<IJwtHelper, JwtHelper>();
builder.Services.AddJwtService(new JwtOptionIOC(builder.Configuration));
builder.Services.AddSingleton<ILoginService, LoginService>();

builder.Services.AddSingleton<IPeopleCountService, PeopleCountService>();
builder.Services.AddSingleton(new InMemoryData());


builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddSocketSessionInterceptor<CustomWebSocketSessionInterceptor>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UploadType>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    ;
var app = builder.Build();

app.UseRouting();
app.UseCors("AllowSpecificOrigins");

app.MapFallbackToFile("index.html");

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets(); //增加webSocket
app.MapGraphQL();
app.MapGraphQLWebSocket("/ws");//增加webSocket扩展

// 配置静态文件中间件
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "", // 基础路径为 "/"
});
app.Urls.Add("http://0.0.0.0:5293");
app.Run();


public class LoginUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class User
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Username { get; set; }
    public string Password { get; set; }
}

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



