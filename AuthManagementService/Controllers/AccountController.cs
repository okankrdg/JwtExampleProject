using AuthManagementService.Models;
using AuthManagementService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthManagementService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService UserService;
        private JwtSettings JwtSettings;
        public AccountController(IUserService userService,IOptions<JwtSettings> jwtSettings)
        {
            UserService = userService;
            JwtSettings = jwtSettings.Value;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = UserService.AuthenticateUser(loginModel.Username, loginModel.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var claims = new List<Claim>();
            claims.Add(new Claim("username", user.Username));
            claims.Add(new Claim("displayname", user.Name));
            var token = JwtHelper.GetJwtToken(user.Username, JwtSettings, new TimeSpan(0, 15, 0),claims.ToArray());
            return Ok(token);
        }
    }
}
