using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
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
	public class AttendanceRecordController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<AttendanceRecord> _attendanceRecordRepository;

		public AttendanceRecordController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_attendanceRecordRepository = new GenericRepository<AttendanceRecord>(_unitOfWork, "AttendanceRecord", "AttendanceId", "Atten");
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetAttendanceRecords(int? employeeId, int? typeId)
		{
			var whereConditions = new Dictionary<string, int>();
			if (employeeId != null) whereConditions.Add("EmployeeID", employeeId.Value);
			if (typeId != null) whereConditions.Add("AttendenceTypeID", typeId.Value);

			var data = await _attendanceRecordRepository.GetListByCustomFields(whereConditions);
			return data;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<AttendanceRecord>> CreateAttendanceRecord(AttendanceRecordDto attendanceRecordDto)
		{
			if (TryValidateModel(attendanceRecordDto))
			{
				var mapping = new MappingHandler();
				var attendanceRecord = mapping.Map<AttendanceRecord>(attendanceRecordDto);
				return await _attendanceRecordRepository.AddAndRetrive(attendanceRecord);
			}
			return BadRequest();
		}
	}
}
