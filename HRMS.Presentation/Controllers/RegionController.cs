using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.ModelsDto;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class RegionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Region> regionRepository;

        public RegionController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            regionRepository = new RegionRepository(_unitOfWork, "Region", "RegionID", "MSORG");
        }

        [HttpGet("[action]/{orgID}")]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegion(int orgID)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>
            {
                { "orgID", orgID }
            };
            var data = await regionRepository.GetListByCustomFields(whereConditionsDic);
            return data;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Region>> CreateRegion(RegionDto Region)
        {
            if (TryValidateModel(Region))
            {
                MappingHandler mapping = new MappingHandler();
                Region _Region = mapping.Map<Region>(Region);
                return await regionRepository.Add(_Region);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateRegion(int id, RegionDto Region)
        {
            if (TryValidateModel(Region))
            {
                MappingHandler mapping = new MappingHandler();
                Region _Region = mapping.Map<Region>(Region);
                return await regionRepository.Update(id, _Region);
            }
            else
                return BadRequest();

        }
    }
}
