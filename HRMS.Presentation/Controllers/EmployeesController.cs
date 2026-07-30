using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using HRMS.DAL.Data;
using HRMS.Presentation.Handlers;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class EmployeesController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public EmployeesController(
            DataContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetEmployeesPagination")]
        public async Task<IActionResult> GetEmployeesPagination(
            [FromQuery] EmployeePaginationRequest request)
        {
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var command = conn.CreateCommand();
            command.CommandText = "Master1_GetEmployeesPagination";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter(
                "@PageNumber",
                request.PageNumber));
            command.Parameters.Add(new SqlParameter(
                "@PageSize",
                request.PageSize));
            command.Parameters.Add(new SqlParameter(
                "@OrgID",
                request.OrgID!.Value));
            command.Parameters.Add(new SqlParameter(
                "@EmployeeID",
                (object?)request.EmployeeID ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter(
                "@SearchValue",
                (object?)request.SearchValue ?? DBNull.Value));

            var totalCountParam = new SqlParameter(
                "@TotalCount",
                SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(totalCountParam);

            var employees = new List<EmployeeDto>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    employees.Add(new EmployeeDto
                    {
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                        EmployeeCode = reader["employeecode"]?.ToString(),
                        FullName = reader["FullName"]?.ToString(),
                        Email = reader["Email"]?.ToString(),
                        Phone = reader["Phone"]?.ToString(),
                        NationalID = reader["NationalID"]?.ToString(),
                        HireDate = reader["HireDate"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["HireDate"]),
                        BirthDate = reader["BirthDate"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["BirthDate"]),
                        DepartmentName = reader["DepartmentName"]?.ToString(),
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        OrgName = reader["OrgName"]?.ToString(),
                        OrgID = Convert.ToInt32(reader["OrgID"]),
                        TitleName = reader["TitleName"]?.ToString(),
                        TitleID = Convert.ToInt32(reader["TitleID"]),
                        BankAccount = reader["BankAccount"]?.ToString(),
                        BankName = reader["BankName"]?.ToString(),
                        Refrence1 = reader["Refrence1"]?.ToString(),
                        Refrence2 = reader["Refrence2"]?.ToString(),
                        Refrence3 = reader["Refrence3"]?.ToString(),
                        Refrence4 = reader["Refrence4"]?.ToString(),
                        Refrence5 = reader["Refrence5"]?.ToString(),
                        Refrence6 = reader["Refrence6"]?.ToString(),
                        Refrence7 = reader["Refrence7"]?.ToString(),
                        Refrence8 = reader["Refrence8"]?.ToString(),
                        Refrence9 = reader["Refrence9"]?.ToString(),
                        Refrence10 = reader["Refrence10"]?.ToString(),
                        Active = reader["Active"] != DBNull.Value
                            && Convert.ToBoolean(reader["Active"]),
                        DateCreated = reader["DateCreated"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["DateCreated"]),
                        LastUpdated = reader["LastUpdated"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["LastUpdated"]),
                        ProjectName = reader["ProjectName"]?.ToString(),
                        Gender = reader["Gender"]?.ToString()
                    });
                }
            }
            // Output parameters are only populated once the reader above is
            // closed, so @TotalCount is read here, after the using block.

            // The single-employee branch of the stored procedure doesn't set
            // @TotalCount, so fall back to the row count in that case.
            var totalCount = request.EmployeeID.HasValue
                ? employees.Count
                : (totalCountParam.Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(totalCountParam.Value));

            if (request.EmployeeID.HasValue && employees.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No employee found with the specified ID."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Employees retrieved successfully",
                totalCount,
                pageNumber = request.PageNumber,
                pageSize = request.PageSize,
                data = employees
            });
        }
    }

    public class EmployeePaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        [Required(ErrorMessage = "OrgID is required.")]
        public int? OrgID { get; set; }

        public int? EmployeeID { get; set; }
        public string? SearchValue { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? NationalID { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? DepartmentName { get; set; }
        public int DepartmentID { get; set; }
        public string? OrgName { get; set; }
        public int OrgID { get; set; }
        public string? TitleName { get; set; }
        public int TitleID { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public string? Refrence6 { get; set; }
        public string? Refrence7 { get; set; }
        public string? Refrence8 { get; set; }
        public string? Refrence9 { get; set; }
        public string? Refrence10 { get; set; }
        public bool Active { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? ProjectName { get; set; }
        public string? Gender { get; set; }
    }
}
