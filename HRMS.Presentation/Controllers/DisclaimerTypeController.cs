using HRMS.DAL;
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
    public class DisclaimerTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<DisclaimerType> _disclaimerTypeRepository;

        public DisclaimerTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _disclaimerTypeRepository = new GenericRepository<DisclaimerType>(_unitOfWork, "DisclaimerType", "DisclaimerTypeID", "Disc");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DisclaimerType>>> GetDisclaimerTypes(int disclaimerTypeId, int orgId)
        {
            var data = await _disclaimerTypeRepository.GetListByCustomFields(new Dictionary<string, int>
            {
                { "DisclaimerTypeID", disclaimerTypeId },
                { "OrgID", orgId }
            });
            return data;
        }
    }
}
