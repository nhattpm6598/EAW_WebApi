using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.Service;
using EAW_WebApi.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using EAW_WebApi.Request;

namespace EAW_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public HomeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getData([FromQuery]IdRequest request)
        {
            ActionResult response = NotFound();
            ////{"empId":"4"}
            try
            {
                //convert input
                int id = request.id;
                //
                HomeService service = new HomeService(_unitOfWork);
                string json = service.getData(id);
                if (json !=null)
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
