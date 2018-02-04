using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeminarMVCServis.DAL.Models;
using SeminarMVCServis.Models;

namespace SeminarMVCServis.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly SeminarContext _context;

        public IConfiguration Configuration { get; set; }

        public TokenController(IConfiguration configuration, SeminarContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.User.Any(c => c.UserName == tokenRequest.UserName && c.UserPIN==tokenRequest.UserPIN))
            {
                JwtSecurityToken token = JwsTokenCreator.CreateToken(tokenRequest.UserName,
            Configuration["Auth:JwtSecurityKey"],
            Configuration["Auth:ValidIssuer"],
            Configuration["Auth:ValidAudience"]);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenStr);
            }
            return Unauthorized();
        }

    }
}