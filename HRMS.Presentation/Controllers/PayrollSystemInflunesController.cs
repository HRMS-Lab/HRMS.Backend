using HRMS.DAL;
using HRMS.DAL.DTOs;
using HRMS.DAL.Handler;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Models;
using HRMS.DAL.UnitOfWork;
using HRMS.Presentation.Handlers;
using HRMS.Presentation.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;


namespace HRMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionHandler]
    [Authorize]
    public class PayrollSystemInflunesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<DAL.PayrollSystemInflunesView> _influnesRepository;

        public PayrollSystemInflunesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _influnesRepository = new GenericRepository<DAL.PayrollSystemInflunesView>(_unitOfWork, "PayrollSystemInflune", "SysInfID", "Pay");
        }


        [HttpGet("[action]/{orgid}")]
        public async Task<ActionResult<IEnumerable<DAL.PayrollSystemInflunesView>>> GetInflunes(int orgid)
        {
            var data = await _influnesRepository.GetListByCustomField(orgid, "OrgID");
            return data;




        }
    }
}
