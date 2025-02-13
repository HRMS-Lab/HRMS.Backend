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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IReadOnlyRepository<UserView> _userViewRepository;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = new GenericRepository<User>(_unitOfWork, "User", "UserID", "Users");
            _userViewRepository = new ReadOnlyRepository<UserView>(_unitOfWork, "User", "UserID", "Users");
        }

        [HttpGet("[action]/{orgId}")]
        public async Task<ActionResult<IEnumerable<UserView>>> GetUsers(int orgId)
        {
            var data = await _userViewRepository.GetListByCustomField(orgId, "orgid");
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<User>> CreateUser(UserDto user)
        {
            if (TryValidateModel(user))
            {
                var mapper = new MappingHandler();
                User _user = mapper.Map<User>(user);
                return await _userRepository.Add(_user);
            }
            return BadRequest();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeUserActivity(int id, ChangeActivityRequest req)
        {
            if (TryValidateModel(req))
            {
                var mapper = new MappingHandler();
                User _user = mapper.Map<User>(req);
                return await _userRepository.Update(id, _user);
            }
            return BadRequest();
        }
    }
}
