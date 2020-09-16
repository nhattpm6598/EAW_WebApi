using EAW_WebApi.Models;
using EAW_WebApi.Service;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EAW_WebApi.Data.UnitOfWork;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using EAW_WebApi.Utilities;

namespace EAW_WebApi.Data.Service
{
    public class LoginService
    {
        private UnitOfWork.UnitOfWork _unitOfWork;
        private IConfiguration _config;
        public LoginService(UnitOfWork.UnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        #region login FireBase
        private object tmp;
        public string loginFireBase(FirebaseToken decodedToken, string tokenFcm)
        {
            string json = null;

            string uid = decodedToken.Uid;

            decodedToken.Claims.TryGetValue("email", out tmp);
            string email = (string)tmp;
            TokenUser tokenUser = new TokenUser
            {
                Username = email,
                Uid = uid,
                FcmToken = tokenFcm
            };
            var employee = _unitOfWork.Repository<Employee>().Find(e => e.Email.Equals(tokenUser.Username));
            if (employee != null)
            {
                var tokenString = CommonUtils.GenerateJSONWebToken(_config, tokenUser);// convert JWT
                tokenUser.JwtToken = tokenString;;
                if (_unitOfWork.Repository<TokenUser>().Find(e => tokenUser.Uid.Equals(e.Uid)) == null)
                {
                    _unitOfWork.Repository<TokenUser>().Insert(tokenUser);//add uid
                    _unitOfWork.Commit();
                }
                else
                {
                    TokenUser updateToken = _unitOfWork.Repository<TokenUser>().Find(e => tokenUser.Uid == e.Uid);
                    updateToken.JwtToken = tokenString;
                    updateToken.FcmToken = tokenFcm;
                    _unitOfWork.Commit();
                }
                JObject newO = new JObject{
                        new JProperty("email", tokenUser.Username),
                        new JProperty("tokenString", tokenString),
                        new JProperty("id", employee.Id),
                        new JProperty("username",employee.Name),
                        new JProperty("image", employee.Image)
                    };//add JWT in json response
                json = JsonConvert.SerializeObject(newO, Formatting.Indented);
            }
            return json;
        }
        #endregion
    }
}
