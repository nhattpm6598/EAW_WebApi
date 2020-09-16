using EAW_WebApi.Data.Service;
using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using EAW_WebApi.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service
{
    public class ScheduleService : IScheduleService
    {
        private UnitOfWork _unitOfWork;
        
        public ScheduleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string getData(ScheduleRequest request)
        {
            return getWorkShiftOfEmp(request);
        }

        #region getDataWorkShiftOfEmp
        public string getWorkShiftOfEmp(ScheduleRequest request)
        {
            //init
            string json = null;
            int id = request.id;
            DateTime first = request.firstDate;
            DateTime last = request.lastDate;
            DateTime nowDate = DateTime.Now;

            if (first == null || last == null)
            {
                first = HomeService.FirstDayOfWeek(nowDate);
                last = HomeService.LastDayOfWeek(nowDate);
            }
            //access data 
            Employee dataBase = getDataWorkShiftByEmpId(id);

            //convert logic
            if (dataBase != null)
            {
                JArray array = new JArray();

                foreach (var items in dataBase.WorkingShift)
                {

                    if (items.ShiftStart.Date <= last && items.ShiftStart.Date >= first)
                    {

                        JObject tmp = new JObject
                        (
                            new JProperty("date", items.ShiftStart.DayOfWeek + "\n" + items.ShiftStart.Day + "-" + items.ShiftStart.Month),
                            new JProperty("checkin", items.ShiftStart.ToString("H:mm")),
                            new JProperty("checkout", items.ShiftEnd.ToString("H:mm")),
                            new JProperty("checkinExpandTime", items.CheckInExpandTime),
                            new JProperty("checkoutExpandTime", items.CheckInExpandTime),
                            new JProperty("status", items.Status),
                            new JProperty("brandName", items.Store.Brand.BrandName),
                            new JProperty("storeName", items.Store.Name),
                            new JProperty("AddressStore", items.Store.Address)

                        );
                        array.Add(tmp);
                    }
                }

                JObject data = new JObject
                (
                    new JProperty("schedule", array)
                );
                // covert jobject to json
                json = JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            

            return json;
        }
        #endregion

        #region *** LINQ *** get Data WorkShift
        private Employee getDataWorkShiftByEmpId(int empId)
        {
            return _unitOfWork.Repository<Employee>().GetAll().Where(e => e.Active == true && e.Id == empId)
                .Include(x => x.WorkingShift)
                    .ThenInclude(y => y.Store)
                        .ThenInclude(z=>z.Brand)
                .FirstOrDefault();
        }
        #endregion

    }
}
