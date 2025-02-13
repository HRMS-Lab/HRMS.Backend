using HRMS.DAL;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.Repository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL.Views;
using HRMS.Presentation.Handlers;
using HRMS.Presentation.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class AdminProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<AdminProjectMapping> adminProjectsRepository;
        IReadOnlyRepository<AdminProjectsView> adminProjectsViewRepository;

        public AdminProjectsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            adminProjectsRepository = new GenericRepository<AdminProjectMapping>(_unitOfWork, "ProjAdmin", "Admin_ProjID", "Atten");
            adminProjectsViewRepository = new ReadOnlyRepository<AdminProjectsView>(_unitOfWork, "ProjAdmin", "Admin_ProjID", "Atten");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AdminProjectMapping>> AssignProjAdmin(AssignAdminProjectsRequest req)
        {
            var projectIDs = string.Join(",", req.ProjectIds);

            return await adminProjectsRepository.ExecuteProcedure("AssignProjAdmins", new Dictionary<string, Object?>
            {
                { "AdminID", req.AdminId },
                { "ProjectIDs", projectIDs }
            }, true);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<AdminProjectsView>>> GetProjAdmins(int? adminId, int? projectId)
        {
            return await adminProjectsViewRepository.GetListByCustomFields(new Dictionary<string, int>
            {
                { "AdminID", adminId ?? 0 },
                { "ProjectID", projectId ?? 0 }
            });
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<AdminProjectsView>> ChangeStatus(UpdateProjAdminRequest req)
        {
            var parameters = new Dictionary<string, Object?>
            {
                { "AdminID", req.AdminId },
                { "ProjectIDs", string.Join(',', req.ProjectIds) },
                { "Active",  Convert.ToInt32(req.IsActive) }
            };

            await adminProjectsRepository.ExecuteProcedure("UpdateProjAdmins", parameters);

            var AdminProjects = new AdminProjectsView();

            var result = await GetProjAdmins(req.AdminId, null);
            if (result.Result is OkObjectResult okResult)
            {
                AdminProjects = (okResult.Value as List<AdminProjectsView>).First(x => x.UserId == req.AdminId);
            }

            return Ok(AdminProjects);
        }
    }
}
