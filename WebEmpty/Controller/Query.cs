
using HotChocolate.Authorization;
using water_api.Services;

namespace WebEmpty.Controller;

[Authorize]
public class Query
{
    private readonly InMemoryData _data;
    private readonly ILoginService _loginService;

    public Query(InMemoryData data,ILoginService loginService)
    {
        _data = data;
        _loginService = loginService;
    }
    [AllowAnonymous]
    public async Task<string> Login(LoginUserDto user)
    {
        try
        {
            var username = user.Username;
            var password = user.Password;
    
            var token = await _loginService.Login(username, password);
            if (token.Equals("-1"))
            {
                return "登录失败";
            }
    
            return token;
        }
        catch (Exception e)
        {
            return "登录失败";
        }
    }

    public IQueryable<Person> GetPeople() => _data.People.AsQueryable();

    public Person GetPerson(string id) => _data.GetPersonById(id);
}