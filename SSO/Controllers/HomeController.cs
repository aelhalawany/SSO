using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSO.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SSO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync()
        {
            var token = await GetTokenAsync();
            var data = await SignIn(token);

            //var claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name,"Ahmed El-Halawany"),
            //    new Claim(ClaimTypes.Upn,"aelhalawany"),
            //    new Claim("tin","3000000001"),
            //    new Claim("nationalid","1000000001")
            //};
            //var claimIdentity = new ClaimsIdentity(claims, "UserIdentity");
            //var userPrincipal = new ClaimsPrincipal(claimIdentity);
            //HttpContext.SignInAsync(userPrincipal);

            return Ok(new { data });

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> GetTokenAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44333/jwt";

                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var token = await Response.Content.ReadAsStringAsync();
                        return token;

                    }
                    else
                    {
                        return string.Empty;

                    }

                }
            }
        }

        private async Task<string> SignIn(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44333/SignIn";
                // Setting Authorization.  
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var Response = await client.GetAsync(endpoint))
                {


                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var data = await Response.Content.ReadAsStringAsync();
                        return data;

                    }
                    else
                    {
                        return string.Empty;

                    }

                }
            }
        }
    }
}
