using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.Repository;
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
    public class SecurityRoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadOnlyRepository<SecurityRoleView> _secRoleViewRepository;
        private readonly IGenericRepository<SecurityRole> _secRoleRepository;

        public SecurityRoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _secRoleViewRepository = new ReadOnlyRepository<SecurityRoleView>(unitOfWork, "SecurityRole", "SecRoleID", "Users");
            _secRoleRepository = new GenericRepository<SecurityRole>(unitOfWork, "SecurityRole", "SecRoleID", "Users");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SecurityRoleView>>> GetSecurityRoles()
        {
            var data = await _secRoleViewRepository.Get();
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SecurityRole>> AddSecurityRole(SecurityRoleDto securityRole)
        {
            if (TryValidateModel(securityRole))
            {
                var mapper = new MappingHandler();
                SecurityRole _secRole = mapper.Map<SecurityRole>(securityRole);
                return await _secRoleRepository.Add(_secRole);
            }
            return BadRequest();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateSecurityRole(int id, SecurityRoleDto securityRole)
        {
            if (TryValidateModel(securityRole))
            {
                var mapper = new MappingHandler();
                SecurityRole _secRole = mapper.Map<SecurityRole>(securityRole);
                return await _secRoleRepository.Update(id, _secRole);
            }
            return BadRequest();
        }
    }
}
