using HRMS.DAL;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.ModelsDto;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using HRMS.Presentation.Infrastructure.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ApiExceptionHandler]
	[Authorize]
	public class AttachmentController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IFileUpload _fileUpload;
		IGenericRepository<Employee> employeeRepository;
		IGenericRepository<Attachment> attachmentRepository;

		public AttachmentController(IUnitOfWork unitOfWork, IFileUpload fileUpload)
		{
			this._unitOfWork = unitOfWork;
			attachmentRepository = new AttachmentRepository(_unitOfWork, "Attachment", "AttachmentID", "MSATT");
			employeeRepository = new EmployeeRepository(_unitOfWork, "Employee", "EmployeeId");
			_fileUpload = fileUpload;
		}

		[HttpGet("[action]/{id}/{orgid}")]
		public async Task<ActionResult<Attachment>> GetAttachment(int id, int orgid)
		{
			var data = await attachmentRepository.GetByTableIdAndCustomField(id, orgid, "orgid");
			return data;
		}


		[HttpGet("[action]/{OrgId}")]
		public async Task<ActionResult<IEnumerable<Attachment>>> GetAttachments(int OrgId)
		{
			var data = await attachmentRepository.GetListByCustomField(OrgId, "orgid");
			return data;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<Attachment>> CreateAttachment([FromForm] AttachmentDto attachment)
		{
			if (TryValidateModel(attachment))
			{
				MappingHandler mapping = new MappingHandler();
				Attachment _attachment = mapping.Map<Attachment>(attachment);

				var employeeResult = await employeeRepository.GetByTableId(attachment.EmployeeId);

				if (employeeResult.Result is OkObjectResult okResult)
				{
					var employee = ((IList<Employee>)okResult.Value)?.FirstOrDefault();

					if (employee == null || attachment.Attachment == null)
						return BadRequest();

					var path = await _fileUpload.UploadFileAsync(attachment.Attachment, employee.EmployeeCode ?? "");
					_attachment.AttachmentPath = path;
					_attachment.FileFormat = attachment.Attachment.ContentType;

					var createResult = await attachmentRepository.Add(_attachment);

					if (createResult.Result is OkObjectResult)
						return createResult;

					//create failed, remove the uploaded file
					try
					{
						await _fileUpload.DeleteFileAsync(path);
					}
					catch { }

					return createResult;
				}
			}

			return BadRequest();
		}


		[HttpPut("[action]/{id}")]
		public async Task<IActionResult> UpdateAttachment(int id, [FromForm] AttachmentDto attachment)
		{
			if (!TryValidateModel(attachment))
				return BadRequest();

			MappingHandler mapping = new MappingHandler();
			Attachment _attachment = mapping.Map<Attachment>(attachment);

			Employee employee;
			var employeeResult = await employeeRepository.GetByTableId(attachment.EmployeeId);
			if (employeeResult.Result is OkObjectResult okResult)
			{
				employee = ((IList<Employee>)okResult.Value).FirstOrDefault();
				if (employee == null)
					return BadRequest();

				var oldAttachmentResult = await attachmentRepository.GetByTableIdAndCustomField(id, employee.OrgId, "orgid");

				if (oldAttachmentResult.Result is OkObjectResult oldAttachmentObjectResult)
				{
					var oldAttachment = (Attachment)oldAttachmentObjectResult.Value;
					if (oldAttachment == null)
						return BadRequest();

					if (attachment.Attachment != null)
					{
						var path = await _fileUpload.UploadFileAsync(attachment.Attachment, employee.EmployeeCode ?? "");
						_attachment.AttachmentPath = path;
						_attachment.FileFormat = attachment.Attachment.ContentType;
					}
					else
					{
						_attachment.AttachmentPath = oldAttachment.AttachmentPath;
						_attachment.FileFormat = oldAttachment.FileFormat;
					}

					var updateResult = await attachmentRepository.Update(id, _attachment);

					if (updateResult is OkObjectResult)
					{
						//upated successfully, remove the old attachment
						try
						{
							await _fileUpload.DeleteFileAsync(oldAttachment.AttachmentPath);
						}
						catch { }
					}
					else
					{
						//something failed delete the uploaded file
						try
						{
							await _fileUpload.DeleteFileAsync(_attachment.AttachmentPath);
						}
						catch { }
					}

					return updateResult;
				}
			}

			return BadRequest();
		}

		[HttpGet("[action]/{id}/{orgId}")]
		public async Task<ActionResult> GetAttachmentFile(int id, int orgId)
		{
			var data = await attachmentRepository.GetByTableIdAndCustomField(id, orgId, "orgid");
			if (data.Result is not OkObjectResult objectResult)
			{
				return NotFound();
			}

			var attachment = (Attachment)objectResult.Value;
			if (attachment == null || string.IsNullOrEmpty(attachment.AttachmentPath))
			{
				return NotFound();
			}

			var path = attachment.AttachmentPath;
			if (!System.IO.File.Exists(path))
			{
				return NotFound();
			}

			var fileBytes = System.IO.File.ReadAllBytes(path);
			var fileName = _fileUpload.GetOriginalFileName(path);

			var contentType = attachment.FileFormat ?? "application/octet-stream";

			return File(fileBytes, contentType, fileName);
		}
	}
}
