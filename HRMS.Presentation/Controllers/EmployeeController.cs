using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Helpers;
using HRMS.DAL.Interfaces;
using HRMS.DAL.ModelsDto;
using HRMS.DAL.Repository;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL.Views;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Security.Cryptography;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        IReadOnlyRepository<EmployeeRegistryView> _employeeRegistryRepository;
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Employee> employeeRepository;
        IReadOnlyRepository<EmployeeDetailsView> _employeeDetailsViewRepository;
        IReadOnlyRepository<EmployeeCountInfoView> _employeeCountInfoViewRepository;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            employeeRepository = new EmployeeRepository(_unitOfWork, "Employee", "EmployeeId");
            _employeeDetailsViewRepository = new ReadOnlyRepository<EmployeeDetailsView>(_unitOfWork, "Employee", "EmployeeId");
            _employeeCountInfoViewRepository = new ReadOnlyRepository<EmployeeCountInfoView>(_unitOfWork, "Employee", "EmployeeId");
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var data = await employeeRepository.GetByTableId(id);
            return data;
        }

        //[HttpGet("[action]/{pageNumber}/{pageSize}/{orgId}")]
        //public async Task<ActionResult<PagedList<EmployeeDetailsView>>> GetEmployees(int pageNumber, int pageSize, int orgid)
        //{
        //    var data = await _employeeDetailsViewRepository.GetPaginated(pageNumber, pageSize, "GetEmployeesPagination", "Employees",orgid.ToString());
        //    return data;
        //}



        //[HttpGet("[action]/{pageNumber}/{pageSize}/{orgId}")]
        //public async Task<ActionResult<PagedList<EmployeeDetailsView>>> GetEmployees(int pageNumber, int pageSize, int orgid)
        //{
        //    var result = _employeeDetailsViewRepository.GetPaginated
        //         .FromSqlRaw(
        //             "EXEC [dbo].[Pay_GetPayrollClaculationHeader] @OrgID = {0}, @PayClacHeaderID = {1}, @Description = {2}, @Month = {3}, @Year = {4}, @Posted = {5}",
        //             orgId, payClacHeaderId, description, month, year, posted)
        //         .ToListAsync();

        //    return Ok(result);
        //}



        //[HttpGet("[action]/{pageNumber}/{pageSize}/filter/{SearchValue}")]
        //public async Task<ActionResult<PagedList<EmployeeDetailsView>>> GetEmployees(int pageNumber, int pageSize, string SearchValue)
        //{
        //    var data = await _employeeDetailsViewRepository.GetPaginated(pageNumber, pageSize, SearchValue, "GetEmployeesPagination", "Employees");
        //    return data;
        //}
        [HttpGet("[action]/{pageNumber}/{pageSize}/filter/{SearchValue}")]
        public async Task<ActionResult<PagedList<EmployeeDetailsView>>> GetEmployees(int pageNumber, int pageSize, string SearchValue)
        {
            // ✅ سحب الـ OrgId من التوكن
            var orgId = User.FindFirst("OrgId")?.Value;

            if (string.IsNullOrEmpty(orgId))
                return Unauthorized("OrgId not found in token");

            // ✅ تمريره للريبوزيتوري
            var data = await _employeeDetailsViewRepository.GetPaginated(pageNumber, pageSize, SearchValue, "GetEmployeesPagination", "Employees");

            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDto Employee)
        {
            if (TryValidateModel(Employee))
            {
                MappingHandler mapping = new MappingHandler();
                Employee _Employee = mapping.Map<Employee>(Employee);
                return await employeeRepository.AddAndRetrive(_Employee);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto Employee)
        {
            if (TryValidateModel(Employee))
            {
                MappingHandler mapping = new MappingHandler();
                Employee _Employee = mapping.Map<Employee>(Employee);
                return await employeeRepository.Update(id, _Employee);
            }
            else
                return BadRequest();

        }

        [HttpGet("[action]/{orgId}")]
        public async Task<ActionResult<IEnumerable<EmployeeCountInfoView>>> CountEmployees(int orgId)
        {
            var data = await _employeeCountInfoViewRepository.GetListByCustomFieldsfilterd(new Dictionary<string, int>
            {
                { "OrgID", orgId }
            }, "", "GetEmployeeCounts");
            return data;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<EmployeeRegistryView>>> GetEmployeesRegistry(
         int? orgId,
         string? employeeCode,
         string? fullName,
         string? nationalId,
         string? phone)
        {
            // Normalize inputs (empty => null)
            employeeCode = string.IsNullOrWhiteSpace(employeeCode) ? null : employeeCode.Trim();
            fullName = string.IsNullOrWhiteSpace(fullName) ? null : fullName.Trim();
            nationalId = string.IsNullOrWhiteSpace(nationalId) ? null : nationalId.Trim();
            phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();

            var parameters = new[]
            {
        new SqlParameter("@OrgID", SqlDbType.Int)
        {
            Value = (object?)orgId ?? DBNull.Value
        },
        new SqlParameter("@EmployeeCode", SqlDbType.NVarChar, 50)
        {
            Value = (object?)employeeCode ?? DBNull.Value
        },
        new SqlParameter("@FullName", SqlDbType.NVarChar, 150)
        {
            Value = (object?)fullName ?? DBNull.Value
        },
        new SqlParameter("@NationalID", SqlDbType.NVarChar, 50)
        {
            Value = (object?)nationalId ?? DBNull.Value
        },
        new SqlParameter("@Phone", SqlDbType.NVarChar, 50)
        {
            Value = (object?)phone ?? DBNull.Value
        }
    };

            try
            {
                var data = await _unitOfWork.Context
                    .Set<EmployeeRegistryView>()
                    .FromSqlRaw("EXEC usp_GetAllEmployeesRegistry @OrgID, @EmployeeCode, @FullName, @NationalID, @Phone", parameters)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

    }
}
