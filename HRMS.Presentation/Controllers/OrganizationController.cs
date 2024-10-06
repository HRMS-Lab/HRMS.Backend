using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.DAL.Handler;
using HRMS.DAL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.DAL.Interfaces;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Organization> organizationRepository;

        public OrganizationController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            organizationRepository = new OrganizationRepository(_unitOfWork, "Organization", "OrgId");
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            var data = await organizationRepository.GetByTableId(id);
            return data;
        }

        
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            var data = await organizationRepository.Get();
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Organization>> CreateOrganization(OrganizationDto organization)
        {
            if (TryValidateModel(organization))
            {
                MappingHandler mapping = new MappingHandler();
                Organization _organization = mapping.Map<Organization>(organization);
                return await organizationRepository.Add(_organization);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateOrganization(int id, OrganizationDto organization)
        {
            if (TryValidateModel(organization))
            {
                MappingHandler mapping = new MappingHandler();
                Organization _organization = mapping.Map<Organization>(organization);
                return await organizationRepository.Update(id, _organization);
            }
            else
                return BadRequest();

        }
    }
}
