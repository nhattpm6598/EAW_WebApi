using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using EAW_WebApi.Service;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EAW_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public AttendanceController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> AttendanceFlutter([FromBody] AttendanceRequest request)
        {
            ActionResult response = NotFound();
            try
            {
                AttendanceService service = new AttendanceService(_unitOfWork);
                Message message = service.CreateMessageOfFutter(request);
                if (message != null)
                {
                    // Send a message to the device corresponding to the provided
                    // registration token.
                    String HTTPProtocol = await FirebaseMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(true);

                    JObject o = new JObject(new JProperty("name", HTTPProtocol));

                    response = Ok(o);
                    // Response is a message ID string.
                }

            }
            catch
            {
                response = BadRequest();
            }
            return response;
        }
    }
}
