using ATMMiniproject.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATMMiniproject.Service
{
    public class TokenService :ITokenService
    {

        private readonly string _secretKey;
        private readonly SymmetricSecurityKey _key;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration configuration , IHttpContextAccessor httpContextAccessor)
        {
            _secretKey = configuration.GetSection("TokenKey").GetSection("JWT").Value.ToString();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateToken(int id)
        {
            string token = string.Empty;
            var claims = new List<Claim>
                {
                    new Claim("id", id.ToString()),
                };
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var myToken = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(myToken);
            return token;
        }


        public bool VerifyPassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
        public int GetUidFromToken()
        {
            var cardId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
            if(cardId == null)
            {
                throw new UnauthorizedAccessException("No Access To the Card");
            }
            if (int.TryParse(cardId, out int cid))
            {
                return cid;
            }
            return 0;
        }
    }
}
