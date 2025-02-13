using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.UnitOfWork;
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
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Role> _roleRepository;

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = new GenericRepository<Role>(_unitOfWork, "Role", "RoleID", "Users");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var data = await _roleRepository.Get();
            return data;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var data = await _roleRepository.GetByTableId(id);
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Role>> CreateRole(RoleDto role)
        {
            if (TryValidateModel(role))
            {
                var mapper = new MappingHandler();
                Role _role = mapper.Map<Role>(role);
                return await _roleRepository.Add(_role);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<Role>> UpdateRole(int id, UpdateRoleRequest req)
        {
            if (TryValidateModel(req))
            {
                var data = await _roleRepository.ExecuteProcedure("UpdateRole", new Dictionary<string, object?>
                {
                    { "RoleID", id },
                    { "RoleName", req.RoleName },
                    { "RoleDescription", req.RoleDescription },
                    { "MarkInactive", req.MarkInActive }
                });
                return data;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
