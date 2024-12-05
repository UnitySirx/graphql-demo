using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebEmpty.jwt
{
    public interface IJwtHelper
    {
        List<Claim> Claims { get; set; }
        string GetJwtToken();
        bool ValidateToken(string token, out ClaimsPrincipal? claimsPrincipal);
        Task<bool> ValidateTokenAsync(string token);
    }

    public class JwtHelper : IJwtHelper
    {
        private readonly IJwtOptionIOC _jwtOptionIOC;

        public JwtHelper(IJwtOptionIOC jwtOptionIOC)
        {
            _jwtOptionIOC = jwtOptionIOC;
        }

        public List<Claim> Claims { get; set; } = new List<Claim>();

        public string GetJwtToken()
        {
            var jwtOption = _jwtOptionIOC.GetJwtOption();

            var jwtSecurityToken = new JwtSecurityToken(jwtOption.Issuer, jwtOption.Audience, Claims,
                jwtOption.NotBefore, jwtOption.Expiration, jwtOption.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }

        public bool ValidateToken(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            claimsPrincipal = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtOptionIOC.GetJwtOption().Issuer,
                    ValidAudience = _jwtOptionIOC.GetJwtOption().Audience,
                    IssuerSigningKey = _jwtOptionIOC.GetJwtOption().SymmetricSecurityKey,
                };

                claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtOptionIOC.GetJwtOption().Issuer,
                    ValidAudience = _jwtOptionIOC.GetJwtOption().Audience,
                    IssuerSigningKey = _jwtOptionIOC.GetJwtOption().SymmetricSecurityKey,
                };

                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                var tokenStr = token.Split(' ')[1];

                var result = await tokenHandler.ValidateTokenAsync(tokenStr, validationParameters);
                return result.IsValid;
            }
            catch
            {
                return false;
            }
        }
    }
}