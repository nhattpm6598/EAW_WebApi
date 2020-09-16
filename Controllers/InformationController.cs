using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using EAW_WebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

namespace EAW_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public InformationController(IConfiguration config, UnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getInfEmp([FromQuery]IdRequest request)
        {
            ActionResult response = NotFound();
            //{"empId":"4"}
            try
            {
                int id = request.id;
                InformationService service = new InformationService(_unitOfWork);
                string json = service.getInformationEmpById(id);
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

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> updateInfEmp([FromQuery] UInforRequest request)
        {
            ActionResult response = null;
            //
            InformationService service = new InformationService(_unitOfWork);
            var res = service.updateInformationEmpById(request);
            //
            switch (res){
                case "notFound":
                    response = NotFound();break;
                case "failed":
                    response = BadRequest();break;
                case "success":
                    response = Ok(service.getInformationEmpById(request.id)); break;
                case null:
                    response = BadRequest();break;

            }
            return await Task.FromResult(response);
        }
    }
}
