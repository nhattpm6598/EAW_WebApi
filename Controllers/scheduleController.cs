using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using EAW_WebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EAW_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public ScheduleController(IConfiguration config, UnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }
        // GET: api/<HistoryController>
        [HttpGet]
        [Authorize]
        public Task<ActionResult> Get([FromQuery] ScheduleRequest request)
        {
            //{"empId":"4"}
            ActionResult response = NotFound();
            string json = null;
            try
            {
                ScheduleService service = new ScheduleService(_unitOfWork);
                json = service.getData(request);
                if (json != null) response = Ok(json);
                else response = NotFound();
            }
            catch
            {
                response = BadRequest();
            }

            //
            return Task.FromResult(response);
        }

    }
}
