using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class DisclaimerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Disclaimer> _disclaimerRepository;

        public DisclaimerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _disclaimerRepository = new GenericRepository<Disclaimer>(_unitOfWork, "Disclaimer", "DisclaimerID", "Disc");
        }

        [HttpGet("[action]/{orgId}")]
        public async Task<ActionResult<IEnumerable<Disclaimer>>> GetDisclaimers(int employeeId, int orgId)
        {
            var data = await _disclaimerRepository.GetListByCustomFields(new Dictionary<string, int>
            {
                { "EmployeeID", employeeId },
                { "OrgID", orgId }
            });
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Disclaimer>> CreateDisclaimer(DisclaimerDto disclaimer)
        {
            if (TryValidateModel(disclaimer))
            {
                var mapper = new MappingHandler();
                Disclaimer _disclaimer = mapper.Map<Disclaimer>(disclaimer);
                return await _disclaimerRepository.Add(_disclaimer);
            }
            return BadRequest();
        }

        [HttpPut("[action]/{disclaimerId}")]
        public async Task<ActionResult<Disclaimer>> UpdateDisclaimerSetInactive(int disclaimerId)
        {
            return await _disclaimerRepository.ExecuteProcedure("UpdateDisclaimerSetInactive", new Dictionary<string, object?>
            {
                { "DisclaimerID", disclaimerId }
            }, true);
        }
    }
}
