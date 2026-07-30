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
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly DataContext _context;

        public AttendanceController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the monthly attendance report for employees, with optional filters.
        /// The stored procedure now builds the JSON itself (FOR JSON PATH) directly
        /// from [dbo].[EmployeesAttendanceCalender], so the API no longer reads rows
        /// into a Dictionary and re-serializes them — it reads the JSON text SQL Server
        /// already produced and returns it as-is. SQL Server splits large FOR JSON
        /// output across multiple result rows (~2033 chars each), so those chunks are
        /// concatenated here before being written to the response.
        /// </summary>
        [HttpGet("GetAttendanceReport")]
        public async Task<IActionResult> GetAttendanceReport(
            [FromQuery] int attendanceYear,
            [FromQuery] int attendanceMonth,
            [FromQuery] string? fullName = null,
            [FromQuery] string? nationalID = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? titleName = null)
        {
            if (attendanceMonth < 1 || attendanceMonth > 12)
            {
                return BadRequest(new { success = false, message = "attendanceMonth must be between 1 and 12." });
            }

            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var command = conn.CreateCommand();
            command.CommandText = "Atten_usp_AttendanceReport";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@AttendanceYear", attendanceYear));
            command.Parameters.Add(new SqlParameter("@AttendanceMonth", attendanceMonth));
            command.Parameters.Add(new SqlParameter("@FullName", (object?)fullName ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@NationalID", (object?)nationalID ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Phone", (object?)phone ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@TitleName", (object?)titleName ?? DBNull.Value));

            var jsonBuilder = new System.Text.StringBuilder();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    // FOR JSON PATH returns the JSON text in a single column,
                    // potentially split across several rows for large results.
                    jsonBuilder.Append(reader.GetString(0));
                }
            }

            // SQL Server returns no rows at all (not even an empty "[]") when the
            // result set is empty, so handle that explicitly.
            var json = jsonBuilder.Length > 0 ? jsonBuilder.ToString() : "[]";

            return Content(json, "application/json");
        }
    }
}