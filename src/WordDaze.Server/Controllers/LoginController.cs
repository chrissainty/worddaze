using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordDaze.Shared;

namespace WordDaze.Server.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost(Urls.Login)]
        public IActionResult Login([FromBody] LoginDetails login)
        {
            if (login.Username == "admin" && login.Password == "SuperSecretPassword")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

                var token = new JwtSecurityToken(
                    _configuration["JwtIssuer"],
                    _configuration["JwtIssuer"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest("Username and password are invalid.");
        }
    }
}