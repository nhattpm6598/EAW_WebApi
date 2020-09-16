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
    public class HistoryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public HistoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<historyController>
        [HttpGet]
        [Authorize]
        public Task<ActionResult> getHistoryAttendance([FromQuery] ScheduleRequest request)
        {
            //{"empId":"4"}
            ActionResult response = NotFound();
            string json = null;
            try
            {
                HistoryService service = new HistoryService(_unitOfWork);
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
