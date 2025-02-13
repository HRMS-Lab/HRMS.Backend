using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
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
    public class BranchEmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<BranchEmployee> _branchEmployeeRepository;

        public BranchEmployeeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _branchEmployeeRepository = new GenericRepository<BranchEmployee>(_unitOfWork, "BranchEmployee", "BranchEmpID", "MSORG");
        }

        [HttpGet("[action]/{orgid}/{branchEmpId}")]
        public async Task<ActionResult<IEnumerable<BranchEmployee>>> GetBranchEmployees(int orgid, int branchEmpId, int employeeId, int branchId)
        {
            var data = await _branchEmployeeRepository.GetListByCustomFields(new Dictionary<string, int>
            {
                { "OrgID", orgid },
                { "EmployeeID", employeeId },
                { "BranchID", branchId },
                { "BranchEmpID", branchEmpId }
            });
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BranchEmployee>> CreateBranchEmployee(BranchEmployeeDto branchEmployee)
        {
            if (TryValidateModel(branchEmployee))
            {
                MappingHandler mapping = new MappingHandler();
                BranchEmployee _branchEmployee = mapping.Map<BranchEmployee>(branchEmployee);
                return await _branchEmployeeRepository.Add(_branchEmployee);
            }
            else
                return BadRequest();
        }

        [HttpPut("[action]/{branchEmpId}")]
        public async Task<IActionResult> UpdateBranchEmployee(int branchEmpId, BranchEmployeeDto branchEmployee)
        {
            if (TryValidateModel(branchEmployee))
            {
                MappingHandler mapping = new MappingHandler();
                BranchEmployee _branchEmployee = mapping.Map<BranchEmployee>(branchEmployee);
                return await _branchEmployeeRepository.Update(branchEmpId, _branchEmployee);
            }
            else
                return BadRequest();
        }
    }
}
