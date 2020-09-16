using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using EAW_WebApi.ReuqestWinform;
using EAW_WebApi.Service;
using EAW_WebApi.ServiceWinform;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace EAW_WebApi.ControllerWinform
{
    [Route("api/v1/Winform/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDistributedCache _redisCache;
        public AttendanceController(UnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _redisCache = cache;
        }

        [HttpPost]
        public async Task<ActionResult> AttendanceWinform([FromBody]AttendanceWinformRequest request)
        {
            ActionResult response = NotFound();
            try
            {
                AttendanceService service = new AttendanceService(_unitOfWork);
                Message message = service.CreateMessageOfWinform(request);
                if(message!= null)
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

        [HttpPost]
        [Route("redis")]
        public async Task<ActionResult> getRedis([FromBody]AttendanceWinformRequest request)
        {
            ActionResult response = NotFound();
            try
            {
                RedisConnectionService service = new RedisConnectionService(_unitOfWork,_redisCache);
                string result = service.updateRedis(request);
                if(!result.Equals(""))
                {
                    response = Ok(result);
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