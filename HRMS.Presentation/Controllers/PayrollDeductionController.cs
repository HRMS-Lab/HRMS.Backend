using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using HRMS.Presentation.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class PayrollDeductionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PayrollDeduction> _payrollDeductionRepository;

        public PayrollDeductionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _payrollDeductionRepository = new GenericRepository<PayrollDeduction>(_unitOfWork, "PayrollDeduction", "PayDeductID", "Pay");
        }

        [HttpGet("[action]/{orgId}")]
        public async Task<ActionResult<IEnumerable<PayrollDeduction>>> GetPayrollDeductions(int orgId)
        {
            var data = await _payrollDeductionRepository.GetListByCustomField(orgId, "OrgID");
            return data;
        }

        [HttpGet("[action]/{orgId}/{payDeductId}")]
        public async Task<ActionResult<PayrollDeduction>> GetPayrollDeduction(int orgId, int payDeductId)
        {
            var data = await _payrollDeductionRepository.GetByTableIdAndCustomField(payDeductId, orgId, "OrgID");
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<PayrollDeduction>> CreatePayrollDeduction(PayrollDeductionDto payDeduct)
        {
            if (TryValidateModel(payDeduct))
            {
                MappingHandler mapping = new MappingHandler();
                PayrollDeduction _payDeduct = mapping.Map<PayrollDeduction>(payDeduct);
                return await _payrollDeductionRepository.Add(_payDeduct);
            }
            else
                return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<PayrollDeduction>> UpdatePayrollDeduction(ChangeActivityWithIdRequest req)
        {
            var data = await _payrollDeductionRepository.ExecuteProcedure("UpdatePayrollDeduction", new Dictionary<string, object?>
            {
                { "PayDeductID", req.Id },
                { "Active", req.Active }
            }, true);

            return await _payrollDeductionRepository.GetByTableId(req.Id);
        }
        //private readonly DbContext _context;

        //public PayrollDeductionController(DbContext context)
        //{
        //    _context = context;
        }
       // [HttpPost("[action]")]
        //public async Task<IActionResult> CreatePayrollDeduction2([FromBody] PayrollDeductionDto payDeduct)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var result = await _context.Database.ExecuteSqlRawAsync(
        //            "EXEC Pay_InsertPayrollDeduction @OrgID = {0}, @DeductionName = {1}, @DeductionDesc = {2}, @Refrence = {3}, @SysInfID = {4}",
        //            payDeduct.OrgId,
        //            payDeduct.DeductionName,
        //            payDeduct.DeductionDesc,
        //            payDeduct.Refrence,
        //            payDeduct.SysInfID
        //        );

        //        return Ok(new { success = true, message = "Deduction inserted successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            success = false,
        //            message = "An error occurred while inserting the deduction.",
        //            error = ex.Message
        //        });
        //    }
        //}
        ////End of code
    }

