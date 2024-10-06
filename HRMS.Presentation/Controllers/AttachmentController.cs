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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class AttachmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Attachment> attachmentRepository;

        public AttachmentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            attachmentRepository = new AttachmentRepository(_unitOfWork, "Attachment", "AttachmentID", "MSATT");
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
        public async Task<ActionResult<Attachment>> CreateAttachment(AttachmentDto attachment)
        {
            if (TryValidateModel(attachment))
            {
                MappingHandler mapping = new MappingHandler();
                Attachment _attachment = mapping.Map<Attachment>(attachment);
                return await attachmentRepository.Add(_attachment);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateAttachment(int id, AttachmentDto attachment)
        {
            if (TryValidateModel(attachment))
            {
                MappingHandler mapping = new MappingHandler();
                Attachment _attachment = mapping.Map<Attachment>(attachment);
                return await attachmentRepository.Update(id, _attachment);
            }
            else
                return BadRequest();

        }

        [HttpPut("[action]/{id}/{orgid}")]
        public async Task<ActionResult<string>> GetAttachmentFile(int id, int orgid)
        {
            var data = await attachmentRepository.GetByTableIdAndCustomField(id, orgid, "orgid");
            ObjectResult objectResult = (ObjectResult)data.Result;
            byte[] image = ((Attachment)objectResult.Value).Attachments;
            return File(image, "image/jpg");

        }
    }
}
