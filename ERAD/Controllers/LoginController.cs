using ERAD.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERAD
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        [HttpPost]
        public void Login(string UserName,string Password)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Ahmed El-Halawany"),
                new Claim(ClaimTypes.Upn,"aelhalawany"),
                new Claim("tin","3000000001"),
                new Claim("nationalid","1000000001")
            };
            var claimIdentity = new ClaimsIdentity(claims, "UserIdentity");
            var userPrincipal = new ClaimsPrincipal(claimIdentity);
            HttpContext.SignInAsync(userPrincipal);

            //return RedirectToAction("Secret");
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
