using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service.Interface
{
    interface IHistoryService
    {
        string getData (ScheduleRequest request);
    }
}
