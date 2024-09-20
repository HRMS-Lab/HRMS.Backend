using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.DAL.Repository;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Project> projectRepository;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            projectRepository = new ProjectRepository(_unitOfWork, "project", "ProjectId", "MSORG");
        }

        [HttpGet("[action]/{orgID}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProject(int orgID)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>
            {
                { "orgID", orgID }
            };
            var data = await projectRepository.GetListByCustomFields(whereConditionsDic);
            return data;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Project>> CreateProject(ProjectDto Project)
        {
            if (TryValidateModel(Project))
            {
                MappingHandler mapping = new MappingHandler();
                Project _Project = mapping.Map<Project>(Project);
                return await projectRepository.Add(_Project);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateRegion(int id, ProjectDto Project)
        {
            if (TryValidateModel(Project))
            {
                MappingHandler mapping = new MappingHandler();
                Project _Project = mapping.Map<Project>(Project);
                return await projectRepository.Update(id, _Project);
            }
            else
                return BadRequest();

        }
    }
}
