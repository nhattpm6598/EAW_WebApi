using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using EAW_WebApi.Service.Interface;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EAW_WebApi.Service
{
    public class NotificationService : INotificationService
    {
        private static UnitOfWork _unitOfWork;
        public NotificationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string getAll(IdRequest request)
        {
            return getListNotificationByEmpId(request);
        }

        #region logic getData *** Notification ***
        private string getListNotificationByEmpId(IdRequest request)
        {
            string json = null;
            int empId = request.id;
            JArray array = new JArray();
            List<Notifications> notificationsList = getDataNotificationByEmpId(empId);

            //convert data
            foreach(var items in notificationsList)
            {
                JObject tmp = new JObject(
                    new JProperty("createDate", items.CreateDate),
                    new JProperty("title", items.Title),
                    new JProperty("mode", items.CheckFace.Mode == 1 ? "Attendance Machine" : 
                                    (items.CheckFace.Mode == 2 ? "Attendance OR code" : 
                                    (items.CheckFace.Mode == 3 ? "Attendance Send Request By StoreManager" : null)))
                    );
                array.Add(tmp);
            }
            //
            JObject data = new JObject(new JProperty("historyNotifications", array));

            json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return json;
        }
        #endregion

        #region data *** notification ***
        private List<Notifications> getDataNotificationByEmpId(int empId)
        {
            if (_unitOfWork.Repository<Employee>().GetAll().Where(x=>x.Active == true && x.Id == empId).FirstOrDefault() != null)
            {
                return _unitOfWork.Repository<Notifications>().GetAll().Where(x => x.EmployeeId == empId)
                    .Include(a => a.CheckFace)
                    .ToList();
            }
            return null;
        }
        #endregion
    }
}
