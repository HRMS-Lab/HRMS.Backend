using HRMS.DAL;
using HRMS.DAL.Data;
using HRMS.DAL.DTOs;
using HRMS.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS.Presentation.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DataContext dataContext;

        public TokenController(IConfiguration config, DataContext context)
        {
            _configuration = config;
            dataContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthenticationRequest _userData)
        {
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.UserName, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    ClaimsIdentity ci = new ClaimsIdentity();
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.UserID.ToString()),
                        new Claim("Username", user.UserName),
                        new Claim("OrganizationID", user.OrganizationID.ToString()),
                        new Claim("OrganizationName", user.OrganizationName.ToString())
                    };



                    var ExpirationDatatime = DateTime.Now.AddYears(4);
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: ExpirationDatatime,
                        signingCredentials: signIn);

                    AuthenticationResponse tokenResponse = new AuthenticationResponse
                    {
                        ExpirationDateTime = ExpirationDatatime,
                        OrgID = user.OrganizationID,
                        OrgName = user.OrganizationName,
                        UserName = user.UserName.ToString(),
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
                    };

                    return Ok(tokenResponse);
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<TokenDto> GetUser(string username, string password)
        {
            User user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
            if (user == null)
            {
                return null;
            }
            {
                Organization organization = await dataContext.Organizations.FirstOrDefaultAsync(o => o.OrgId == user.OrgID);
                TokenDto loginObject = new TokenDto
                {
                    OrganizationID = organization.OrgId,
                    OrganizationName = organization.OrgName,
                    UserID = user.UserID,
                    UserName = user.UserName
                };

                return loginObject;
            }
        }
    }
}
