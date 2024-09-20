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

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
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
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var data = await employeeRepository.GetByTableId(id);
            return data;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(int pageNumber, int pageSize)
        {
            //Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>();
            //whereConditionsDic.Add("PageSize", pageSize);
            //whereConditionsDic.Add("PageNumber", pageSize);
            //var data = await employeeRepository.GetListByCustomFields(whereConditionsDic);

            var data = await employeeRepository.Get();
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDto Employee)
        {
            if (TryValidateModel(Employee))
            {
                MappingHandler mapping = new MappingHandler();
                Employee _Employee = mapping.Map<Employee>(Employee);
                return await employeeRepository.Add(_Employee);
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
