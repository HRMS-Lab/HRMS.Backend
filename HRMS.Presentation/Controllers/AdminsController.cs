using HRMS.DAL.Interfaces;
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
	public class AdminsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		IReadOnlyRepository<UserIdentityView> userIdentityViewRepository;

		public AdminsController(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			userIdentityViewRepository = new ReadOnlyRepository<UserIdentityView>(_unitOfWork, "User", "UserID", "Atten");
		}

		[HttpGet("[action]/{orgId}")]
		public async Task<ActionResult<IEnumerable<UserIdentityView>>> GetSupervisers(int orgId)
		{
			var data = await userIdentityViewRepository.GetListByCustomFieldsfilterd(new Dictionary<string, int>
			{
				{ "OrgID", orgId }
			}, "", "SelectSupervisers");
			return data;
		}
	}
}
