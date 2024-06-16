using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sep490_G60_Backend_SmartBuildPC.Service
{
    public class TokenGenerate
    {
        private readonly IConfiguration _configuration;

        private readonly IAccountRepository _repository;

        public TokenGenerate(IConfiguration configuration, IAccountRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        public async Task<string> generateToken(LoginFormRequest userRequest)
        {

            var account = await _repository.GetAccount(userRequest.email, userRequest.password);
            string Id = account.AccountId.ToString();
            string Email = account.Email;
            string Role = account.AccountType;

            var jwtToken = new JwtSecurityTokenHandler();

            var secretKeyEncrypt = Encoding.UTF8.GetBytes(_configuration.GetSection("Config").GetSection("SecretKey").Value);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID",Id),
                    new Claim(ClaimTypes.Email,Email),
                    new Claim("TokenId",Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddDays(40),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyEncrypt),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Config").GetSection("SecretKey").Value)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
