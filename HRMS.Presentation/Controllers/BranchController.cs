using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Repository;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL.Views;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
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
        IReadOnlyRepository<BranchView> branchViewRepository;

        public BranchController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            branchRepository = new BranchRepository(_unitOfWork, "Branch", "BranchID", "MSORG");
            branchViewRepository = new ReadOnlyRepository<BranchView>(_unitOfWork, "Branch", "BranchID", "MSORG");
        }



        [HttpGet("[action]/{orgid}/{BranchID}")]
        public async Task<ActionResult<BranchView>> GetBranch(int orgid, int BranchID)
        {
            var data = await branchViewRepository.GetByTableIdAndCustomField(BranchID, orgid, "OrgID");
            return data;
        }

        [HttpGet("[action]/{orgid}")]
        public async Task<ActionResult<IEnumerable<BranchView>>> GetBranchs(int orgid)
        {
            var data = await branchViewRepository.GetListByCustomField(orgid, "OrgID");
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
