using EAW_WebApi.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace EAW_WebApi.Utilities
{
    interface ICommonUltils
    {
        static string GenerateJSONWebToken(IConfiguration _config, TokenUser user)
        {
            throw new NotImplementedException();
        }
        
        static Boolean checkAuthorization()
        {
            throw new NotImplementedException();
        }
    }
}
