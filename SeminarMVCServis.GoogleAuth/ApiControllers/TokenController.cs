using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeminarMVCServis.DAL.Models;
using SeminarMVCServis.GoogleAuth.Models;

namespace SeminarMVCServis.GoogleAuth.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/Token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly RestaurantsContext _context;

        public IConfiguration Configuration { get; set; }

        public TokenController(IConfiguration configuration, RestaurantsContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.Users.Any(c => c.UserName == tokenRequest.UserName && c.UserPIN==tokenRequest.UserPIN))
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

        [HttpGet("RequestTokenVersion")]
        [HttpPost("RequestTokenVersion")]
        [MapToApiVersion("1.0"), MapToApiVersion("1.1")]
        public string GetApiVersion() => HttpContext.GetRequestedApiVersion().ToString();

    }

    [Produces("application/json")]
    [ApiVersion("1.1")]
    [Route("api/Token")]
    [AllowAnonymous]
    public class TokenV1_1Controller : Controller
    {
        private readonly RestaurantsContext _context;

        public IConfiguration Configuration { get; set; }

        public TokenV1_1Controller(IConfiguration configuration, RestaurantsContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.Users.Any(c => c.UserName == tokenRequest.UserName && c.ApsUserId == tokenRequest.UserStringId))
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