using EAW_WebApi.ReuqestWinform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.ServiceWinform.Interface
{
    interface IRedisConnectionService
    {
        string updateRedis(AttendanceWinformRequest request);
    }
}
