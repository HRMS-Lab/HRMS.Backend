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

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class OrganizationChartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<OrganizationChart> organizationchartRepository;

        public OrganizationChartController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            organizationchartRepository = new OrganizationChartRepository(_unitOfWork, "OrganizationChart", "OrgChartID", "MSORG");
        }

        [HttpGet("[action]/{id}/{orgId}")]
        public async Task<ActionResult<OrganizationChart>> GetOrganizationChart(int id, int orgId)
        {
            var data = await organizationchartRepository.GetByTableIdAndCustomField(id, orgId, "orgid");
            return data;
        }


        [HttpGet("[action]/{OrgId}")]
        public async Task<ActionResult<IEnumerable<OrganizationChart>>> GetOrganizationCharts(int OrgId)
        {
            var data = await organizationchartRepository.GetListByCustomField(OrgId, "orgid");
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<OrganizationChart>> CreateOrganizationChart(OrganizationChartDto organizationchart)
        {
            if (TryValidateModel(organizationchart))
            {
                MappingHandler mapping = new MappingHandler();
                OrganizationChart _organizationchart = mapping.Map<OrganizationChart>(organizationchart);
                return await organizationchartRepository.Add(_organizationchart);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateOrganizationChart(int id, OrganizationChartDto organizationchart)
        {
            if (TryValidateModel(organizationchart))
            {
                MappingHandler mapping = new MappingHandler();
                OrganizationChart _organizationchart = mapping.Map<OrganizationChart>(organizationchart);
                return await organizationchartRepository.Update(id, _organizationchart);
            }
            else
                return BadRequest();

        }
    }
}
