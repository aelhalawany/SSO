using ERADapi.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERADapi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class JwtController : Controller
    {
        [HttpGet]
        public IActionResult Jwt()
        {
            return new ObjectResult(JwtToken.GetJwtToken());
        }
    }
}
