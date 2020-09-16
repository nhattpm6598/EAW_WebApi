using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EAW_WebApi.ControllersAdmin
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public EmployeesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<ActionResult> getData()
        {
            ActionResult response = NotFound();
            //{"empId":"4"}
            try
            {
                ServiceAdmin.EmployeeService service = new ServiceAdmin.EmployeeService(_unitOfWork);
                string json = service.getTotalHourOfEmployeeByStore();
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
