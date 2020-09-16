using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service.Interface
{
    interface LoginInterface
    {
        //string loginJWT(UnitOfWork _unitOfWork, string tokenJWT);
        string loginFireBase(FirebaseToken decodedToken, string tokenFcm);
    }
}
