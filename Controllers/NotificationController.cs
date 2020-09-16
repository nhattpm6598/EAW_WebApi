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
    public class NotificationController : ControllerBase
    {

        private IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public NotificationController(IConfiguration config, UnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getNotifEmp([FromQuery] IdRequest request)
        {
            ActionResult response = NotFound();
            //{"empId":"4"}
            try
            {
                NotificationService service = new NotificationService(_unitOfWork);
                string json = service.getAll(request);
                //
                if (json != null)
                {
                    response = Ok(json);
                }
            }
            catch
            {
                response = BadRequest();
            }
            return await Task.FromResult(response);
        }

    }
}
