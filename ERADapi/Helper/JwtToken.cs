using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERADapi.Helper
{
    public static class JwtToken
    {
        public static string GetJwtToken()
        {
            var claims = new[]
          {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("granny", "cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);
            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload 
            {
                {"some_id","some_id"},
                {"granny","cookie"},
                {"exp",DateTime.Now.AddMinutes(1) },
                {"iss",Constants.Issuer },
                {"aud",Constants.Audiance }
            };
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenstring = handler.WriteToken(secToken);
            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            return handler.WriteToken(token);
        }
    }
}
