using HRMS.DAL.DTOs;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using HRMS.DAL.Data;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
   

    public class PayrollContractController : Controller
    {
        private readonly DataContext _context;

        public PayrollContractController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("create-contract")]

        public async Task<IActionResult> CreatePayrollContract([FromBody] PayrollContractHeaderDto contract)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("PayTempLinesID", typeof(int));
            dataTable.Columns.Add("AmountTemp", typeof(double));
            dataTable.Columns.Add("Amount", typeof(double));
            dataTable.Columns.Add("AmountOverride", typeof(bool));

            foreach (var line in contract.Lines)
            {
                bool amountOverride = (line.AmountTemp.HasValue && line.Amount.HasValue && line.AmountTemp.Value != line.Amount.Value);

                dataTable.Rows.Add(
                   line.PayTempLinesID,
            line.AmountTemp.HasValue ? (object)line.AmountTemp.Value : DBNull.Value,
            line.Amount.HasValue ? (object)line.Amount.Value : DBNull.Value,
            amountOverride
        );

            }
            ;
          //  DbContext _context = new DbContext();
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var command = conn.CreateCommand();
            command.CommandText = "Pay_InsertPayrollContract";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@EmployeeID", contract.EmployeeID));
            command.Parameters.Add(new SqlParameter("@PayTempHeadID", contract.PayTempHeadID));
            command.Parameters.Add(new SqlParameter("@StartDate", contract.StartDate));
            command.Parameters.Add(new SqlParameter("@EndDate", contract.EndDate));
            command.Parameters.Add(new SqlParameter("@Descrption", contract.Descrption ?? ""));
            command.Parameters.Add(new SqlParameter("@Active", contract.Active));

            var linesParam = new SqlParameter("@Lines", dataTable)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.PayrollContractLinesType"
            };
            command.Parameters.Add(linesParam);

            var newID = await command.ExecuteScalarAsync();

            return Ok(new { success = true, PayContHeadID = newID });
        }

        [HttpGet("GetPayrollContractWithDetails")]
       public async Task<IActionResult> GetPayrollContractWithDetails(
     [FromQuery] int orgId,
     [FromQuery] int? payTempHeadId = null,
     [FromQuery] int? payContHeadId = null,
     [FromQuery] int? employeeId = null)
        {
            var contracts = await _context
                .Set<PayrollContractWithDetailsDto>()
                .FromSqlRaw(
                    "EXEC Pay_GetPayrollContractWithDetails @OrgID = {0}, @PayTempHeadID = {1}, @PayContHeadID = {2}, @EmployeeID = {3}",
                    orgId,
                    payTempHeadId,
                    payContHeadId,
                    employeeId)
                .ToListAsync();

            return Ok(contracts);
        }


    }
}
