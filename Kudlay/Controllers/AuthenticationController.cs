using BAL.AdminService;
using BAL.ModelsDTO;
using Kudlay.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kudlay.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly CustomSettings _settings;
        private readonly IAdminService _adminService;
        public AuthenticationController(IConfiguration configuration, IAdminService adminService)
        {
            _settings = new CustomSettings();
            configuration.Bind(_settings);
            _adminService = adminService;
        }

        [HttpPost]
        [Route("authentication-user")]
        public IActionResult AuthenticationAdmin(Login login)
        {
            if(login.LoginAdmin != null && login.PasswordAdmin != null)
            {
                AdminDTO admin = new AdminDTO
                {
                    Login = login.LoginAdmin,
                    Password = login.PasswordAdmin
                };
                
                if(_adminService.CheckAdmin(admin))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTSettings.SecretKey));

                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOption = new JwtSecurityToken(

                        issuer: _settings.JWTSettings.Host,
                        audience: _settings.JWTSettings.Host,
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signingCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return BadRequest(new { Message = "Админ с такими данными отсутствует" });
                }
            }
            else
            {
                return BadRequest(new { Message = "Неправильный ввод!" });
            }

        }

    }
}
