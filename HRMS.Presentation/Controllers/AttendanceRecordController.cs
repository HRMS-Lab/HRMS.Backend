using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
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
    public class AttendanceRecordController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<AttendanceRecord> _attendanceRecordRepository;
        IReadOnlyRepository<AttenRecordView> attenRecordViewRepository;

        public AttendanceRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attendanceRecordRepository = new GenericRepository<AttendanceRecord>(_unitOfWork, "AttendanceRecord", "AttendanceID", "Atten");
            attenRecordViewRepository = new ReadOnlyRepository<AttenRecordView>(_unitOfWork, "AttendanceRecord", "AttendanceID", "Atten");
        }

        [HttpGet("[action]/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttenRecordView>>> GetAttendanceRecords(int employeeId, int? attenTypeId, DateTime? fromDate, DateTime? toDate)
        {
            if ((fromDate != null && toDate == null) || (fromDate == null && toDate != null))
                return BadRequest("Both fromDate and toDate are required");

            var whereDict = new Dictionary<string, object?>
            {
                { "EmployeeID", employeeId },
                { "AttendenceTypeID", attenTypeId },
                { "datefiterFrom", fromDate },
                { "datefiterto", toDate }
            };

            var data = await attenRecordViewRepository.GetListByCustomFields(whereDict);
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AttendanceRecord>> CreateAttendanceRecord(AttendanceRecordDto attendanceRecordDto)
        {
            if (TryValidateModel(attendanceRecordDto))
            {
                var mapping = new MappingHandler();
                var attendanceRecord = mapping.Map<AttendanceRecord>(attendanceRecordDto);
                return await _attendanceRecordRepository.Add(attendanceRecord);
            }
            return BadRequest();
        }
    }
}
