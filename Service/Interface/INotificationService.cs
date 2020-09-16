using EAW_WebApi.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service.Interface
{
    interface INotificationService
    {
        string getAll(IdRequest request);
    }
}
