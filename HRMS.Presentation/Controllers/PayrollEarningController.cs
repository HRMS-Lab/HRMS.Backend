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
    public class PayrollEarningController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PayrollEarning> _payrollEarningRepository;

        public PayrollEarningController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _payrollEarningRepository = new GenericRepository<PayrollEarning>(_unitOfWork, "PayrollEarning", "PayEarningID", "Pay");
        }

        [HttpGet("[action]/{orgId}")]
        public async Task<ActionResult<IEnumerable<PayrollEarning>>> GetPayrollEarnings(int orgId)
        {
            var data = await _payrollEarningRepository.GetListByCustomField(orgId, "OrgID");
            return data;
        }

        [HttpGet("[action]/{orgId}/{payEarningId}")]
        public async Task<ActionResult<PayrollEarning>> GetPayrollEarning(int orgId, int payEarningId)
        {
            var data = await _payrollEarningRepository.GetByTableIdAndCustomField(payEarningId, orgId, "OrgID");
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<PayrollEarning>> CreatePayrollEarning(PayrollEarningDto payEarning)
        {
            if (TryValidateModel(payEarning))
            {
                MappingHandler mapping = new MappingHandler();
                PayrollEarning _payEarning = mapping.Map<PayrollEarning>(payEarning);
                return await _payrollEarningRepository.Add(_payEarning);
            }
            else
                return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<PayrollEarning>> UpdatePayrollEarning(ChangeActivityWithIdRequest req)
        {
            var data = await _payrollEarningRepository.ExecuteProcedure("UpdatePayrollEarning", new Dictionary<string, object?>
            {
                { "PayEarningID", req.Id },
                { "Active", req.Active }
            }, true);

            return await _payrollEarningRepository.GetByTableId(req.Id);
        }
    }
}
