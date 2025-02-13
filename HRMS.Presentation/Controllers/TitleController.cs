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
