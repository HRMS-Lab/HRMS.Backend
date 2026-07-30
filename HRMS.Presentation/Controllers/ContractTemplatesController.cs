using HRMS.DAL.Data;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using static HRMS.Presentation.Controllers.ContractTemplateLineRequest;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class ContractTemplatesController : Controller
    {
        private readonly DataContext _context;

        public ContractTemplatesController(DataContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Creates a contract template with header and payroll influence lines.
        /// </summary>
        [HttpPost("InsertContractTemplateWithLines")]
        public async Task<IActionResult> InsertContractTemplateWithLines(
            [FromBody] ContractTemplateRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request."
                });
            }


            if (request.Lines == null || request.Lines.Count == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "At least one contract line is required."
                });
            }


            var linesJson = JsonSerializer.Serialize(request.Lines);


            var conn = _context.Database.GetDbConnection();

            await conn.OpenAsync();


            using var command = conn.CreateCommand();

            command.CommandText = "usp_InsertContractTemplateWithLines";

            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("@ContractName",
                request.ContractName));


            command.Parameters.Add(new SqlParameter("@ProjectID",
                request.ProjectID));


            command.Parameters.Add(new SqlParameter("@TitleID",
                request.TitleID));


            command.Parameters.Add(new SqlParameter("@OrgID",
                request.OrgID));


            command.Parameters.Add(new SqlParameter("@Notes",
                (object?)request.Notes ?? DBNull.Value));


            command.Parameters.Add(new SqlParameter("@EffectiveFrom",
                request.EffectiveFrom));


            command.Parameters.Add(new SqlParameter("@EffectiveTo",
                (object?)request.EffectiveTo ?? DBNull.Value));


            command.Parameters.Add(new SqlParameter("@IsDefault",
                request.IsDefault));


            command.Parameters.Add(new SqlParameter("@CreatedBy",
                request.CreatedBy));


            command.Parameters.Add(new SqlParameter("@ContractLinesJSON",
                linesJson));



            Guid contractTemplateID;


            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    contractTemplateID = reader.GetGuid(0);
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Contract was not created."
                    });
                }
            }


            return Ok(new
            {
                success = true,
                message = "Contract template created successfully.",
                contractTemplateID
            });
        }
        /// <summary>
        /// Returns all active contract templates with their lines.
        /// Optionally filters by contract name.
        /// </summary>
        [HttpGet("GetActiveContractTemplates")]
        public async Task<IActionResult> GetActiveContractTemplates(
            [FromQuery] string? contractName = null)
        {
            var conn = _context.Database.GetDbConnection();

            await conn.OpenAsync();

            using var command = conn.CreateCommand();

            command.CommandText = "usp_GetActiveContractTemplates";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ContractName",
                (object?)contractName ?? DBNull.Value));

            var headers = new List<ContractTemplateHeaderDto>();
            var lines = new List<ContractTemplateLineDto>();

            using var reader = await command.ExecuteReaderAsync();

            //==========================
            // First Result Set (Header)
            //==========================
            while (await reader.ReadAsync())
            {
                headers.Add(new ContractTemplateHeaderDto
                {
                    ContractTemplateID = reader.GetGuid(reader.GetOrdinal("ContractTemplateID")),
                    ContractName = reader["ContractName"]?.ToString(),
                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                    ProjectName = reader["ProjectName"]?.ToString(),
                    TitleID = Convert.ToInt32(reader["TitleID"]),
                    TitleName = reader["TitleName"]?.ToString(),
                    OrgID = Convert.ToInt32(reader["OrgID"]),
                    OrgName = reader["OrgName"]?.ToString(),
                    Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    EffectiveFrom = Convert.ToDateTime(reader["EffectiveFrom"]),
                    EffectiveTo = reader["EffectiveTo"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["EffectiveTo"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    IsDefault = Convert.ToBoolean(reader["IsDefault"]),
                    VersionNo = Convert.ToInt32(reader["VersionNo"])
                });
            }

            //=========================
            // Second Result Set (Lines)
            //=========================
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    lines.Add(new ContractTemplateLineDto
                    {
                        ContractLineID = reader.GetGuid(reader.GetOrdinal("ContractLineID")),
                        ContractTemplateID = reader.GetGuid(reader.GetOrdinal("ContractTemplateID")),
                        PayInfID = Convert.ToInt32(reader["PayInfID"]),
                        PayInfoName = reader["PayInfoName"]?.ToString(),
                        Amount = reader["Amount"] == DBNull.Value
                                    ? null
                                    : Convert.ToDecimal(reader["Amount"]),
                        CalculationType = Convert.ToInt32(reader["CalculationType"]),
                        Percentage = reader["Percentage"] == DBNull.Value
                                        ? null
                                        : Convert.ToDecimal(reader["Percentage"]),
                        IsMandatory = Convert.ToBoolean(reader["IsMandatory"]),
                        SequenceNo = reader["SequenceNo"] == DBNull.Value
                                        ? 0
                                        : Convert.ToInt32(reader["SequenceNo"]),
                        Notes = reader["Notes"] == DBNull.Value
                                    ? null
                                    : reader["Notes"].ToString()
                    });
                }
            }

            return Ok(new
            {
                success = true,
                headers,
                lines
            });
        }
    }



    public class ContractTemplateRequest
    {
        public string ContractName { get; set; } = string.Empty;

        public int ProjectID { get; set; }

        public int TitleID { get; set; }

        public int OrgID { get; set; }

        public string? Notes { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsDefault { get; set; }

        public int CreatedBy { get; set; }


        public List<ContractTemplateLineRequest> Lines { get; set; }
            = new();
    }



    public class ContractTemplateLineRequest
    {
        public int PayInfID { get; set; }

        public decimal? Amount { get; set; }

        public int CalculationType { get; set; } = 1;

        public decimal? Percentage { get; set; }

        public bool IsMandatory { get; set; } = true;

        public int SequenceNo { get; set; } = 0;

        public string? Notes { get; set; }


       

        public class ContractTemplateHeaderDto
        {
            public Guid ContractTemplateID { get; set; }

            public string? ContractName { get; set; }

            public int ProjectID { get; set; }

            public string? ProjectName { get; set; }

            public int TitleID { get; set; }

            public string? TitleName { get; set; }

            public int OrgID { get; set; }

            public string? OrgName { get; set; }

            public string? Notes { get; set; }

            public DateTime EffectiveFrom { get; set; }

            public DateTime? EffectiveTo { get; set; }

            public bool IsActive { get; set; }

            public bool IsDefault { get; set; }

            public int VersionNo { get; set; }
        }

        public class ContractTemplateLineDto
        {
            public Guid ContractLineID { get; set; }

            public Guid ContractTemplateID { get; set; }

            public int PayInfID { get; set; }

            public string? PayInfoName { get; set; }

            public decimal? Amount { get; set; }

            public int CalculationType { get; set; }

            public decimal? Percentage { get; set; }

            public bool IsMandatory { get; set; }

            public int SequenceNo { get; set; }

            public string? Notes { get; set; }
        }
    }
}