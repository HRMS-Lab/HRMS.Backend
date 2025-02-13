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
    }
}
