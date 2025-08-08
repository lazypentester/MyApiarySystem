using Auth.Api.Models;
using Auth.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Api.Tokens
{
    public static class AccessToken
    {
        public static string GenerateJWT(Account user, double expires, IOptions<AuthOptions> authOptions)
        {
            var authParams = authOptions.Value;

            var securitykey = authParams.GetSymmetricSecurityKey();
            var creditials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Mail),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", user.Role)
            };

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                //expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                expires: DateTime.Now.AddSeconds(expires),
                signingCredentials: creditials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
