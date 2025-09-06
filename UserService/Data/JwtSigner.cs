using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserService.Data
{
    public class JwtSigner
    {
        private readonly SymmetricSecurityKey key;
        private readonly string issuer;
        private readonly string audience;

        public JwtSigner(string key, string issuer, string audience)
        {
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            this.issuer = issuer;
            this.audience = audience;
        }

        public string Create(Guid id)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, id.ToString()) }),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}