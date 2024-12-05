using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebEmpty.jwt
{

    public static class JwtService
    {
        public static void AddJwtService(this IServiceCollection services, IJwtOptionIOC _jwtOptionIOC)
        {
            services.AddAuthentication(option =>
            {
                //认证middleware配置
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //Token颁发机构
                    ValidIssuer = _jwtOptionIOC.GetJwtOption().Issuer,
                    //颁发给谁
                    ValidAudience = _jwtOptionIOC.GetJwtOption().Audience,
                    //这里的key要进行加密
                    IssuerSigningKey = _jwtOptionIOC.GetJwtOption().SymmetricSecurityKey,
                    //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                };
            });
            services.AddAuthorization();
        }
    }
}
