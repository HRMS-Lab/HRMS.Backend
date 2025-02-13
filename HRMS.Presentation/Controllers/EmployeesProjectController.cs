using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
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
	public class EmployeesProjectController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		IGenericRepository<EmployeesProject> employeesprojectRepository;
		IReadOnlyRepository<EmployeeInfoView> employeesinfoviewRepository;

		public EmployeesProjectController(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			employeesprojectRepository = new EmployeesProjectRepository(_unitOfWork, "EmployeesProject", "EmpProjId", "MSORG");
			employeesinfoviewRepository = new ReadOnlyRepository<EmployeeInfoView>(_unitOfWork, "Employee", "EmployeeID", "Atten");
		}

		[HttpGet("[action]")]

		public async Task<ActionResult<IEnumerable<EmployeeInfoView>>> GetEmployeesProject(int projectId, int employeeId)
		{
			var data = await employeesinfoviewRepository.GetListByCustomFieldsfilterd(new Dictionary<string, int>
			{
				{ "ProjectID", projectId },
				{ "EmployeeID", employeeId }
			}, "", "GetEmployeesbyprojects");

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
