using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.DAL.Handler;
using HRMS.DAL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.DAL.Interfaces;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Employee> employeeRepository;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            employeeRepository = new EmployeeRepository(_unitOfWork, "Employee", "EmployeeId");
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(int id)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>();
            whereConditionsDic.Add("EmployeeID", id);
            whereConditionsDic.Add("PageSize", 10);
            whereConditionsDic.Add("PageNumber", 1);
            var data = await employeeRepository.GetListByCustomFieldsfilterd(whereConditionsDic, string.Empty, "GetEmployeesPagination");
            //var data = await employeeRepository.GetByTableId(id);
            return data;
        }

        [HttpGet("[action]/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(int pageNumber, int pageSize)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>();
            whereConditionsDic.Add("PageSize", pageSize);
            whereConditionsDic.Add("PageNumber", pageNumber);
            var data = await employeeRepository.GetListByCustomFieldsfilterd(whereConditionsDic, string.Empty, "GetEmployeesPagination");
            return data;
        }



        [HttpGet("[action]/{pageNumber}/{pageSize}/filter/{SearchValue}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(int pageNumber, int pageSize, string SearchValue)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>();
            whereConditionsDic.Add("PageSize", pageSize);
            whereConditionsDic.Add("PageNumber", pageNumber);
            var data = await employeeRepository.GetListByCustomFieldsfilterd(whereConditionsDic, SearchValue, "GetEmployeesPagination");
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
    }
}
