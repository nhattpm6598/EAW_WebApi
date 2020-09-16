using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EAW_WebApi.Data.Service;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace EAW_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public LoginController(IConfiguration config, UnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] loginRequest request)
        {
            IActionResult response = Unauthorized();
            string tokenFireBase = null;
            string tokenFcm = null;

            try
            {
                tokenFireBase = request.firebaseToken;
                tokenFcm = request.tokenFcm;
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(tokenFireBase);
                LoginService service = new LoginService(_unitOfWork, _config);
                string json = service.loginFireBase(decodedToken, tokenFcm);
                if (json != null)
                {
                    response = Ok(json);
                }
                else
                {
                    response = NotFound();
                }
            }
            catch
            {
                response = Unauthorized();
            }
            return response;
        }

    }
}
