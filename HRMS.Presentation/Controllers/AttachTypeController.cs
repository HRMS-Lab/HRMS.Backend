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
    public class AttachTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<AttachType> attachtypeRepository;

        public AttachTypeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            attachtypeRepository = new AttachTypeRepository(_unitOfWork, "AttachType", "AttachID", "MSATT");
        }

        [HttpGet("[action]/{id}/{orgid}")]
        public async Task<ActionResult<AttachType>> GetAttachType(int id, int orgid)
        {
            var data = await attachtypeRepository.GetByTableIdAndCustomField(id, orgid, "orgid");
            return data;
        }


        [HttpGet("[action]/{orgid}")]
        public async Task<ActionResult<IEnumerable<AttachType>>> GetAttachTypes(int orgid)
        {
            var data = await attachtypeRepository.GetListByCustomField(orgid,"orgid");
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AttachType>> CreateAttachType(AttachTypeDto attachtype)
        {
            if (TryValidateModel(attachtype))
            {
                MappingHandler mapping = new MappingHandler();
                AttachType _attachtype = mapping.Map<AttachType>(attachtype);
                return await attachtypeRepository.Add(_attachtype);
            }
            else
                return BadRequest();

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateAttachType(int id, AttachTypeDto attachtype)
        {
            if (TryValidateModel(attachtype))
            {
                MappingHandler mapping = new MappingHandler();
                AttachType _attachtype = mapping.Map<AttachType>(attachtype);
                return await attachtypeRepository.Update(id, _attachtype);
            }
            else
                return BadRequest();

        }
    }
}
