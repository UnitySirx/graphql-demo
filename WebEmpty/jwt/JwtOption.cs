using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebEmpty.jwt
{
    public class JwtOption
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间（min）
        /// </summary>
        public int Expired { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime NotBefore => DateTime.Now;

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration => DateTime.Now.AddMinutes(Expired);

        /// <summary>
        /// 密钥Bytes
        /// </summary>
        private SecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        /// <summary>
        /// 加密后的密钥，使用HmacSha256加密
        /// </summary>
        public SigningCredentials SigningCredentials => new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

        /// <summary>
        /// 认证用的密钥
        /// </summary>
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }

    public interface IJwtOptionIOC
    {
        public JwtOption GetJwtOption();
    }

    public class JwtOptionIOC : IJwtOptionIOC
    {
        private readonly IConfiguration _configuration;

        public JwtOptionIOC(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtOption GetJwtOption()
        {
            JwtOption jwtOption = new JwtOption()
            {
                SecretKey = _configuration["JwtOption:SecretKey"],
                Issuer = _configuration["JwtOption:Issuer"],
                Audience = _configuration["JwtOption:Audience"],
                Expired = int.Parse(_configuration["JwtOption:Expired"]),
            };
            return jwtOption;
        }
    }

}
