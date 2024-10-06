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
    public class DistrictController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<District> districtRepository;

        public DistrictController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            districtRepository = new DistrictRepository(_unitOfWork, "District", "regionid", "MSORG");
        }

        [HttpGet("[action]/{regionid}/{orgid}")]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrict(int regionid, int orgid)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>
            {
                { "regionid", regionid },
                { "orgid", orgid }
            };
            var data = await districtRepository.GetListByCustomFields(whereConditionsDic);
            return data;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<District>> CreateDistrict(DistrictDto District)
        {
            if (TryValidateModel(District))
            {
                MappingHandler mapping = new MappingHandler();
                District _District = mapping.Map<District>(District);
                return await districtRepository.Add(_District);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateDistrict(int id, DistrictDto District)
        {
            if (TryValidateModel(District))
            {
                MappingHandler mapping = new MappingHandler();
                District _District = mapping.Map<District>(District);
                return await districtRepository.Update(id, _District);
            }
            else
                return BadRequest();

        }
    }
}
