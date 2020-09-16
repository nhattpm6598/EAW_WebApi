using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using EAW_WebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;


namespace EAW_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RequestController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> insertRequest([FromBody]WorkShiftRequest request)
        {
            IActionResult response = NotFound();
            try
            {
                RequestWorkShiftService service = new RequestWorkShiftService(_unitOfWork);
                string json = service.insertRequest(request);
                if (json.Equals("success"))
                {
                    response = Ok("success");
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
