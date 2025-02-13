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
    public class SecurityGroupsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SecurityGroup> _secGroupRepository;

        public SecurityGroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _secGroupRepository = new GenericRepository<SecurityGroup>(_unitOfWork, "SecurityGroup", "SecurityGroupID", "Users");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SecurityGroup>>> GetSecurityGroups()
        {
            var data = await _secGroupRepository.Get();
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SecurityGroup>> CreateSecurityGroup(SecurityGroupDto securityGroup)
        {
            if (TryValidateModel(securityGroup))
            {
                var mapper = new MappingHandler();
                SecurityGroup _securityGroup = mapper.Map<SecurityGroup>(securityGroup);
                return await _secGroupRepository.Add(_securityGroup);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<SecurityGroup>> UpdateSecurityGroup(ChangeActivityWithIdRequest req)
        {
            var data = await _secGroupRepository.ExecuteProcedure("UpdateSecurityGroup", new Dictionary<string, object?>
            {
                { "SecurityGroupID", req.Id },
                { "Active", req.Active }
            }, true);

            return data;
        }
    }
}
