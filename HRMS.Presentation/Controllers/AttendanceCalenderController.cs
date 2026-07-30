using HRMS.DAL;
using HRMS.DAL.DTOs;
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
    public class AttendanceCalenderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<AttendanceCalender> _attenCalenderRepository;
        private readonly IReadOnlyRepository<AttendanceCalenderView> _attenCalenderViewRepository;

        public AttendanceCalenderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attenCalenderRepository = new GenericRepository<AttendanceCalender>(unitOfWork, "AttendanceCalender", "AttendanceCalenderID", "Atten");
            _attenCalenderViewRepository = new ReadOnlyRepository<AttendanceCalenderView>(unitOfWork, "AttendanceCalender", "AttendanceCalenderID", "Atten");
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<AttendanceCalenderView>>> GetAttendanceCalenders()
        {
            var data = await _attenCalenderViewRepository.Get();
            return data;
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<AttendanceCalender>> LockUnlockAttendanceCalender(AttendanceCalenderDto attenCalender)
        {
            if (TryValidateModel(attenCalender))
            {
                var data = await _attenCalenderRepository.ExecuteProcedure("LockUnlockAttendanceCalendar", new Dictionary<string, object?>
                {
                    { "Day", attenCalender.Day },
                    { "Month", attenCalender.Month },
                    { "Year", attenCalender.Year },
                    { "LockFlag", attenCalender.Lock}
                }, true);

                return data;
            }

            return BadRequest();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAttendanceReport(AttendanceReportDto report)
        {
            if (!TryValidateModel(report))
                return BadRequest();

            var data = await _attenCalenderRepository.ExecuteProcedure(
                "usp_AttendanceReport",
                new Dictionary<string, object?>
                {
            { "AttendanceYear", report.AttendanceYear },
            { "AttendanceMonth", report.AttendanceMonth },
            { "FullName", string.IsNullOrWhiteSpace(report.FullName) ? null : report.FullName },
            { "NationalID", string.IsNullOrWhiteSpace(report.NationalID) ? null : report.NationalID },
            { "Phone", string.IsNullOrWhiteSpace(report.Phone) ? null : report.Phone },
            { "TitleName", string.IsNullOrWhiteSpace(report.TitleName) ? null : report.TitleName }
                },
                false);

            return Ok(data);
        }
        public class AttendanceReportDto
        {
            public int AttendanceYear { get; set; }
            public int AttendanceMonth { get; set; }

            public string? FullName { get; set; }
            public string? NationalID { get; set; }
            public string? Phone { get; set; }
            public string? TitleName { get; set; }
        }
    }
}
