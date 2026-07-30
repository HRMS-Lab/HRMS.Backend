using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using HRMS.DAL.Data;
using HRMS.DAL.DTOs;
namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class PayrollCalculationHeaderController : Controller
    {
        private readonly DataContext _context;

        public PayrollCalculationHeaderController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("GetPayrollCalculationHeader")]
        public async Task<ActionResult<List<PayrollCalculationHeaderDto>>> GetPayrollCalculationHeader(
        [FromQuery] int orgId,
        [FromQuery] int? payClacHeaderId = null,
        [FromQuery] string? description = null,
        [FromQuery] int? month = null,
        [FromQuery] int? year = null,
        [FromQuery] bool? posted = null)
        {
            var result = await _context.Set<PayrollCalculationHeaderDto>()
                .FromSqlRaw(
                    "EXEC [dbo].[Pay_GetPayrollClaculationHeader] @OrgID = {0}, @PayClacHeaderID = {1}, @Description = {2}, @Month = {3}, @Year = {4}, @Posted = {5}",
                    orgId, payClacHeaderId, description, month, year, posted)
                .ToListAsync();

            return Ok(result);
        }
        [HttpPost("InsertPayrollCalculationHeader")]
        public async Task<ActionResult<PayrollCalculationSummaryDto>> InsertPayrollCalculationHeader(
    [FromBody] PayrollCalculationHeaderInsertDto request)
        {
            //var results = await _context.Set<PayrollCalculationHeaderInsertResultDto>()
            var result = await _context
                    .Set<PayrollCalculationSummaryDto>()
                .FromSqlRaw(
                    "EXEC [dbo].[Pay_InsertPayrollClaculationHeader] @OrgID = {0}, @Description = {1}, @Month = {2}, @Year = {3}",
                    request.OrgID, request.Description, request.Month, request.Year)
                .ToListAsync();

           // var result = results.FirstOrDefault();

            if (result == null)
                return NotFound("No result returned from stored procedure.");

            return Ok(result);
        }

        [HttpPost("InsertPayrollCalculations")]
        public async Task<IActionResult> InsertPayrollCalculations([FromBody] PayrollCalculationInsertDto dto)
        {
            try
            {
                var headerIdParam = new SqlParameter("@PayClacHeaderID", dto.PayClacHeaderID);

                var result = await _context
                    .Set<PayrollCalculationSummaryDto>()
                    .FromSqlRaw("EXEC dbo.Pay_InsertPayrollClaculations @PayClacHeaderID", headerIdParam)
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Payroll calculations inserted successfully.",
                    headerClacId = dto.PayClacHeaderID,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred.",
                    error = ex.Message
                });
            }
        }

    }
}

    

