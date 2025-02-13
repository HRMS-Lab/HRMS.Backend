using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
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
    public class RolesUserInterfaceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadOnlyRepository<RoleUserInterfaceView> _roleUiViewRepository;
        private readonly IGenericRepository<RoleUserInterfaces> _roleUiRepository;

        public RolesUserInterfaceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleUiViewRepository = new ReadOnlyRepository<RoleUserInterfaceView>(_unitOfWork, "RolesUserInterface", "UIRoleID", "Users");
            _roleUiRepository = new GenericRepository<RoleUserInterfaces>(_unitOfWork, "RolesUserInterface", "UIRoleID", "Users");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<RoleUserInterfaceView>>> GetRoleUserInterfaces()
        {
            var data = await _roleUiViewRepository.Get();
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<RoleUserInterfaces>> CreateRolesUserInterface(RolesUserInterfaceDto roleUi)
        {
            if (TryValidateModel(roleUi))
            {
                var mapper = new MappingHandler();
                RoleUserInterfaces _roleUi = mapper.Map<RoleUserInterfaces>(roleUi);
                return await _roleUiRepository.Add(_roleUi);
            }
            return BadRequest();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeRoleUiActivity(int id, ChangeActivityRequest req)
        {
            if (TryValidateModel(req))
            {
                var mapper = new MappingHandler();
                RoleUserInterfaces _roleUi = mapper.Map<RoleUserInterfaces>(req);
                return await _roleUiRepository.Update(id, _roleUi);
            }
            return BadRequest();
        }
    }
}
