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
    public class UserInterfacesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserInterface> _userInterfaceRepository;

        public UserInterfacesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userInterfaceRepository = new GenericRepository<UserInterface>(_unitOfWork, "UserInterface", "UIID", "Users");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UserInterface>>> GetUserInterfaces()
        {
            var data = await _userInterfaceRepository.Get();
            return data;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<UserInterface>> GetUserInterface(int id)
        {
            var data = await _userInterfaceRepository.GetByTableId(id);
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserInterface>> CreateUserInterface(UserInterfaceDto userInterface)
        {
            if (TryValidateModel(userInterface))
            {
                var mapper = new MappingHandler();
                UserInterface _ui = mapper.Map<UserInterface>(userInterface);
                return await _userInterfaceRepository.Add(_ui);
            }
            return BadRequest();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeUiActivity(int id, ChangeActivityRequest req)
        {
            if (TryValidateModel(req))
            {
                var mapper = new MappingHandler();
                UserInterface _ui = mapper.Map<UserInterface>(req);
                return await _userInterfaceRepository.Update(id, _ui);
            }
            return BadRequest();
        }
    }
}
