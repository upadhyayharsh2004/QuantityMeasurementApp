using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppServices.Services
{
    public class JsonWebTokenService
    {
        private readonly IConfiguration jwtConfiguration;
        public JsonWebTokenService(IConfiguration authConfiguration)
        {
            this.jwtConfiguration = authConfiguration;
        }
        public string GenerateToken(PersonEntity userEntity)
        {
            string userHiddenKey = jwtConfiguration["Jwt:SecretKey"]!;
            string jwtIssuer = jwtConfiguration["Jwt:Issuer"]!;
            string tokenAudience = jwtConfiguration["Jwt:Audience"]!;
            int validMinutes = int.Parse(jwtConfiguration["Jwt:ExpiryMinutes"]!);

            SymmetricSecurityKey userSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userHiddenKey));

            SigningCredentials credentials = new SigningCredentials(userSignKey, SecurityAlgorithms.HmacSha256);

            // Add claims
            List<Claim> claimsJwtToken = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEntity.EntityId.ToString()),new Claim(JwtRegisteredClaimNames.Email, userEntity.EntityEmail ?? ""),new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),new Claim("name", userEntity.EntityName ?? ""),new Claim("userId", userEntity.EntityId.ToString())
};
            JwtSecurityToken jwtCreatedToken = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: tokenAudience,
                claims: claimsJwtToken,
                expires: DateTime.UtcNow.AddMinutes(validMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtCreatedToken);
        }

        public int GetExpirySeconds()
        {
            int minutesRemain = int.Parse(jwtConfiguration["Jwt:ExpiryMinutes"]!);
            return minutesRemain * 60;
        }
    }
}