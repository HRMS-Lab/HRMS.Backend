using HRMS.DAL;
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

        [HttpGet("[action]/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PagedList<EmployeeDetailsView>>> GetEmployees(int pageNumber, int pageSize)
        {
            var data = await _employeeDetailsViewRepository.GetPaginated(pageNumber, pageSize, "GetEmployeesPagination", "Employees");
            return data;
        }



        [HttpGet("[action]/{pageNumber}/{pageSize}/filter/{SearchValue}")]
        public async Task<ActionResult<PagedList<EmployeeDetailsView>>> GetEmployees(int pageNumber, int pageSize, string SearchValue)
        {
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
    }
}
