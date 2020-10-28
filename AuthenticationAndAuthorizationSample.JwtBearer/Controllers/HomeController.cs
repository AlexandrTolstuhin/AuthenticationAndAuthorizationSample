using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AuthenticationAndAuthorizationSample.JwtBearer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Subject"),
                new Claim(JwtRegisteredClaimNames.Email, "subject@example.com")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);

            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Issuer, 
                Constants.Audience, 
                claims, 
                DateTime.Now, 
                DateTime.Now.AddHours(1), 
                signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);

            ViewBag.Token = value;

            return View();
        }
    }
}