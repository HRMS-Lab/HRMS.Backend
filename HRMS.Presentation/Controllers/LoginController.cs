using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using HRMS.DAL.Data;
using HRMS.Presentation.Handlers;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class LoginController : Controller
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        public LoginController(
            DataContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {


            var conn = _context.Database.GetDbConnection();

            await conn.OpenAsync();


            using var command = conn.CreateCommand();

            command.CommandText = "usp_LoginUser";

            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter(
                "@UserName",
                request.UserName));


            command.Parameters.Add(new SqlParameter(
                "@Password",
                request.Password));



            LoginResponse? user = null;


            using (var reader = await command.ExecuteReaderAsync())
            {

                if (await reader.ReadAsync())
                {

                    user = new LoginResponse
                    {

                        UserID = Convert.ToInt32(reader["UserID"]),

                        OrgID = Convert.ToInt32(reader["OrgID"]),

                        UserName = reader["UserName"].ToString(),

                        FullName = reader["FullName"]?.ToString(),

                        OrgName = reader["OrgName"].ToString(),

                        SecurityGroupID =
                            reader["SecurityGroupID"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(reader["SecurityGroupID"])

                    };

                }

            }



            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid username or password"
                });
            }



            // Generate JWT Token
            var token = GenerateToken(user);



            return Ok(new
            {
                success = true,
                message = "Login successful",

                token,

                user
            });


        }



        private string GenerateToken(LoginResponse user)
        {

            var tokenUrl =
                _configuration["JWT:TokenUrl"];


            // If you already have /api/token service
            // call it here instead of duplicating JWT logic

            return tokenUrl ?? "";

        }

    }



    public class LoginRequest
    {
        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";
    }



    public class LoginResponse
    {

        public int UserID { get; set; }

        public int OrgID { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? OrgName { get; set; }

        public int? SecurityGroupID { get; set; }

    }

}