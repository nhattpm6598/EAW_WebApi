using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Request;
using EAW_WebApi.ReuqestWinform;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service.Interface
{
    interface IAttendanceService
    {
        public Message CreateMessageOfWinform(AttendanceWinformRequest request);
        public Message CreateMessageOfFutter(AttendanceRequest request);

        /*public async Task Send(string notification)
        {
            throw new NotImplementedException();
        }

        public static string getAndroidMessage(string title, object data, string regId)
        {
            throw new NotImplementedException();
        }*/
    }
}
