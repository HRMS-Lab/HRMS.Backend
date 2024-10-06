using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.ModelsDto;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Branch> branchRepository;

        public BranchController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            branchRepository = new BranchRepository(_unitOfWork, "Branch", "EmployeeID", "MSORG");
        }

        

        [HttpGet("[action]/{orgid}/{BranchID}")]
        public async Task<ActionResult<Branch>> GetBranch(int orgid, int BranchID)
        {

            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>
            {
                { "orgid", orgid },
                {"BranchID", BranchID }
            };
            var data = await branchRepository.GetByCustomFields(whereConditionsDic);
            return data;
        }

        [HttpGet("[action]/{orgid}")]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranchs(int orgid)
        {

            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>
            {
                { "orgid", orgid }
            };
            var data = await branchRepository.GetListByCustomFields(whereConditionsDic);
            return data;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Branch>> CreateBranch(BranchDto Branch)
        {
            if (TryValidateModel(Branch))
            {
                MappingHandler mapping = new MappingHandler();
                Branch _Branch = mapping.Map<Branch>(Branch);
                return await branchRepository.Add(_Branch);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateBranch(int id, BranchDto Branch)
        {
            if (TryValidateModel(Branch))
            {
                MappingHandler mapping = new MappingHandler();
                Branch _Branch = mapping.Map<Branch>(Branch);
                return await branchRepository.Update(id, _Branch);
            }
            else
                return BadRequest();

        }
    }
}
