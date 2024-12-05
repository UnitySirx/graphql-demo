using System.Security.Claims;
using WebEmpty.jwt;


namespace water_api.Services;

public interface ILoginService
{
    Task<string> Login(string username, string password);
    Task<bool> ValidateToken(string token);
}

public class LoginService : ILoginService
{
    private readonly IJwtHelper _jwtHelper;

    public LoginService(IJwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }

    public async Task<string> Login(string username, string password)
    {
        var user = new User() { Username = "username", Password = "password" };

        if (username.Equals(user.Username) == false || password.Equals(user.Password) == false)
        {
            return "-1";
        }
        
        if (user == null) return "-1";
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Tester"),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };
        _jwtHelper.Claims = claims;
        var token = _jwtHelper.GetJwtToken();
        return token;
    }

    public async Task<bool> ValidateToken(string token)
    {
        return await _jwtHelper.ValidateTokenAsync(token);
    }
}