using HRMS.DAL;
using HRMS.DAL.Interfaces;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ApiExceptionHandler]
	[Authorize]
	public class AttendanceTypeController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<AttendanceType> _attendanceTypeRepository;

		public AttendanceTypeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_attendanceTypeRepository = new GenericRepository<AttendanceType>(_unitOfWork, "AttendenceType", "AttendenceTypeID", "Atten");
		}

		[HttpGet("[action]/{id}")]
		public async Task<ActionResult<AttendanceType>> GetAttendanceType(int id)
		{
			var data = await _attendanceTypeRepository.GetByTableId(id);
			return data;
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<IEnumerable<AttendanceType>>> GetAttendanceTypes()
		{
			var data = await _attendanceTypeRepository.Get();
			return data;
		}
	}
}
