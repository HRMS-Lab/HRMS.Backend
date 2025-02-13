using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.Repository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL.Views;
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
    public class PayrollTemplateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadOnlyRepository<PayrollTemplateView> _payrollTemplateViewRepository;
        private readonly IGenericRepository<PayrollTemplateHeader> _payrollTemplateHeaderRepository;

        public PayrollTemplateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _payrollTemplateViewRepository = new ReadOnlyRepository<PayrollTemplateView>(_unitOfWork, "PayrollTemplate", "PayTempHeadID", "Pay");
            _payrollTemplateHeaderRepository = new GenericRepository<PayrollTemplateHeader>(_unitOfWork, "PayrollTemplate", "PayTempHeadID", "Pay");
        }

        [HttpGet("[action]/{orgId}")]
        public async Task<ActionResult<IEnumerable<PayrollTemplateView>>> GetPayrollTemplates(int orgId, int payTempHeadId)
        {
            var data = await _payrollTemplateViewRepository.GetListByCustomFields(new Dictionary<string, int>
            {
                { "PayTempHeadID" , payTempHeadId },
                { "OrgID", orgId }
            });
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<PayrollTemplateHeader>> CreatePayrollTemplate(PayrollTemplateDto payrollTemplate)
        {
            if (TryValidateModel(payrollTemplate))
            {
                var mapper = new MappingHandler();
                var _payrollTemplate = mapper.Map<PayrollTemplateHeader>(payrollTemplate);
                return await _payrollTemplateHeaderRepository.Add(_payrollTemplate, new Dictionary<string, IEnumerable<object>>
                {
                    { "Lines", payrollTemplate.PayrollTemplateLines }
                });
            }
            else
                return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<PayrollTemplateHeader>> UpdatePayrollTemplateHeader(ChangeActivityWithIdRequest req)
        {
            var data = await _payrollTemplateHeaderRepository.ExecuteProcedure("UpdatePayrollTemplateHeader", new Dictionary<string, object?>
            {
                { "PayTempHeadID", req.Id },
                { "Active", req.Active }
            }, true);

            return Ok(new PayrollTemplateHeader());
        }
    }
}
