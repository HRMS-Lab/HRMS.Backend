using HRMS.DAL;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.ModelsDto;
using HRMS.DAL.TypeRepository;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Department> departmentRepository;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            departmentRepository = new DepartmentRepository(_unitOfWork, "Department", "DepartmentId");
        }

        [HttpGet("[action]/{id}/{orgid}")]
        public async Task<ActionResult<Department>> GetDepartment(int id, int orgid)
        {
            var data = await departmentRepository.GetByTableIdAndCustomField(id, orgid, "orgid");
            return data;
        }


        [HttpGet("[action]/{OrgId}")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments(int OrgId)
        {
            var data = await departmentRepository.GetListByCustomField(OrgId, "orgid");
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Department>> CreateDepartment(DepartmentDto department)
        {
            if (TryValidateModel(department))
            {
                MappingHandler mapping = new MappingHandler();
                Department _department = mapping.Map<Department>(department);
                return await departmentRepository.Add(_department);
                //return await departmentRepository.AddParam(_department, "InsertDepartment");
                /*
				var nameParam = new SqlParameter("DepartmentName", SqlDbType.NVarChar) { Value = _department.DepartmentName };
				var discriptionParam = new SqlParameter("DepartmentDescription", SqlDbType.NVarChar) { Value = _department.DepartmentDescription };
				var OrgIdParam = new SqlParameter("OrgID", SqlDbType.Int) { Value = _department.OrgId };
				var activeParam = new SqlParameter("Active", SqlDbType.Int) { Value = _department.Active == true ? 1 : 0 };

				var data = await departmentRepository.ExecuteInsertStoredProcedure("InsertDepartment",
					nameParam, discriptionParam, OrgIdParam, activeParam);

				return data;
				*/
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDto department)
        {
            if (TryValidateModel(department))
            {
                MappingHandler mapping = new MappingHandler();
                Department _department = mapping.Map<Department>(department);
                return await departmentRepository.Update(id, _department);
            }
            else
                return BadRequest();

        }
    }
}
