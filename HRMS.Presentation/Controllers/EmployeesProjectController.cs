using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.ModelsDto;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class EmployeesProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<EmployeesProject> employeesprojectRepository;

        public EmployeesProjectController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            employeesprojectRepository = new EmployeesProjectRepository(_unitOfWork, "EmployeesProject", "EmpProjId", "MSORG");
        }

        [HttpGet("[action]")]

        public async Task<ActionResult<IEnumerable<EmployeesProject>>> GetEmployeesProject(int? Id, int? ProjectID, int? EmployeeID)
        {
            Dictionary<string, int> whereConditionsDic = new Dictionary<string, int>();
            if (Id is null) { Id = 0; } else { whereConditionsDic.Add("EmpProjID", (int)Id); }
            if (ProjectID is null) { ProjectID = 0; } else { whereConditionsDic.Add("ProjectID", (int)ProjectID); }
            if (EmployeeID is null) { EmployeeID = 0; } else { whereConditionsDic.Add("EmployeeID", (int)EmployeeID); }

            var data = await employeesprojectRepository.GetListByCustomFields(whereConditionsDic);
            return data;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<EmployeesProject>> CreateEmployeesProject(EmployeesProjectDto EmployeesProject)
        {
            if (TryValidateModel(EmployeesProject))
            {
                MappingHandler mapping = new MappingHandler();
                EmployeesProject _EmployeesProject = mapping.Map<EmployeesProject>(EmployeesProject);
                return await employeesprojectRepository.Add(_EmployeesProject);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateEmployeesProject(int id, EmployeesProjectDto EmployeesProject)
        {
            if (TryValidateModel(EmployeesProject))
            {
                MappingHandler mapping = new MappingHandler();
                EmployeesProject _EmployeesProject = mapping.Map<EmployeesProject>(EmployeesProject);
                return await employeesprojectRepository.Update(id, _EmployeesProject);
            }
            else
                return BadRequest();

        }
    }
}
