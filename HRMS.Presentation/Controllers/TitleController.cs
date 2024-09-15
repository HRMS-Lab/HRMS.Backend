using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.DAL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.TypeRepository;
using HRMS.DAL.UnitOfWork;
using HRMS.DAL;
using HRMS.Presentation.Handlers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    public class TitleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<Title> titlesRepository;

        public TitleController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            titlesRepository = new TitleRepository(_unitOfWork, "Title", "TitleId");
        }



        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Title>> GetTitle(int id)
        {
            var data = await titlesRepository.GetByTableId(id);
            return data;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitles()
        {
            var data = await titlesRepository.Get();
            return data;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Title>> CreateTitle(TitleDto titles)
        {
            if (TryValidateModel(titles))
            {
                MappingHandler mapping = new MappingHandler();
                Title title = mapping.Map<Title>(titles);
                return await titlesRepository.Add(title);
            }
            else
                return BadRequest();

        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateTitle(int id, TitleDto titles)
        {
            if (TryValidateModel(titles))
            {
                MappingHandler mapping = new MappingHandler();
                Title title = mapping.Map<Title>(titles);
                return await titlesRepository.Update(id, title);
            }
            else
                return BadRequest();

        }

    }
}
