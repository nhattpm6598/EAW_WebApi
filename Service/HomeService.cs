using EAW_WebApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Data.Service
{
    public class HomeService : Interface.IHomeService
    {
        private UnitOfWork.UnitOfWork _unitOfWork;
        public HomeService(UnitOfWork.UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string getData(int empId)
        {
            return getEmpAndWorkShiftByEmail(empId);
        }

        #region getEmpAndWorkShiftByEmail
        private string getEmpAndWorkShiftByEmail(int empId)
        {
            string json = null;
            //init
            DateTime nowDate = getDateTimeHanoiVietName();
            DateTime firstOfWeek = FirstDayOfWeek(nowDate);
            DateTime lastOfWeek = LastDayOfWeek(nowDate);

            //access data
            Employee dataBase = getDataWorkShiftByEmpId(empId);

            //convert data to JObject
            if (dataBase != null)
            {
                JObject nextWorkShift = null;
                JObject lastAttendance = null;

                double totalTimeDouble = 0;

                
                foreach (var items in dataBase.WorkingShift)
                {
                    #region total hour
                    if (DateTime.Compare(items.ShiftStart.Date,lastOfWeek.Date)<=0 && DateTime.Compare(items.ShiftStart.Date,firstOfWeek.Date) >=0 )
                    {
                        //nhưng ngày nhỏ hơn nowday và có status là true (đã duoc xác nhân cua manager)
                        if ( DateTime.Compare(items.ShiftStart.Date,nowDate.Date)<=0 && items.Status == true)
                        {
                            TimeSpan time = TimeSpan.Parse(items.TotalWorkTime.ToString());
                            totalTimeDouble += time.TotalHours;
                        }
                    }
                    #endregion

                    #region new Workshift
                    if (nextWorkShift == null && DateTime.Compare(items.ShiftStart,nowDate) >  0)
                    {
                        nextWorkShift = new JObject
                        {
                            new JProperty("workDate", items.ShiftStart.ToString("dd/MM/yyyy")),
                            new JProperty("Start", items.ShiftStart.ToString("H:mm")),
                            new JProperty("End", items.ShiftEnd.ToString("H:mm")),
                            new JProperty("totalTime",items.TotalWorkTime),
                            new JProperty("storeName",items.Store.Name),
                            new JProperty("address", items.Store.Address)
                        };
                    }
                    #endregion
                }

                foreach(var items in dataBase.CheckFace)
                {   
                    #region lastAttendance
                    if (DateTime.Compare(items.CreateTime, nowDate)<=0)
                    {
                        lastAttendance = new JObject(
                            new JProperty("date", items.CreateTime.DayOfWeek + "\n" + items.CreateTime.Day + "-" + items.CreateTime.Month),
                            new JProperty("time",items.CreateTime.ToString("H:mm")),
                            new JProperty("Store", items.StoreId != null ? getDataStoreNameByStoreId(items.StoreId.Value) : null),
                            new JProperty("mode", items.Mode == 1 ? "camera" : (items.Mode == 2 ? "QRcode" : (items.Mode == 3 ? "request" : null))),
                            new JProperty("status", items.Statuts == 1 ? "new" : (items.Statuts == 2 ? "Approved" : (items.Statuts == 3 ? "Reject" : null)))
                            );
                        
                    }
                    #endregion
                }

                JObject data = new JObject
                {
                    new JProperty("empCode",dataBase.EmployeeCode),
                    new JProperty("StoreName", getDataStoreNameByStoreId(dataBase.MainStoreId)),
                    new JProperty("totalTimeDouble",totalTimeDouble),
                    new JProperty("lastAttendance", lastAttendance),
                    new JProperty("NextWorkShift",nextWorkShift)
                };

                //convert Jobject to string json
                json = JsonConvert.SerializeObject(data, Formatting.Indented);
            }

            return json;
        }
        #endregion

        #region *** LINQ *** 

        #region  *** LINQ ***  get Data WorkShift by EmpId
        private Employee getDataWorkShiftByEmpId(int empId)
        {
            return _unitOfWork.Repository<Employee>().GetAll().Where(x => x.Active == true && x.Id == empId)
                .Include(y => y.WorkingShift)
                    .ThenInclude(z => z.Store)
                        .ThenInclude(j => j.Brand)
                .Include(y=>y.CheckFace)
                    .ThenInclude(z=>z.FaceScanMachine)
                        .ThenInclude(j=>j.Store)
                .FirstOrDefault();
        }
        #endregion

        #region  *** LINQ *** get Data Name Store By Store Id
        private string getDataStoreNameByStoreId(int StoreId)
        {
            return _unitOfWork.Repository<Store>().Find(x => x.Active == true && x.Id == StoreId).Name;
        }
        #endregion

        #endregion

        #region start of week end end of week
        public static DateTime FirstDayOfWeek(DateTime date)
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - date.DayOfWeek;
            DateTime fdowDate = date.AddDays(offset);
            return fdowDate.AddDays(1);
        }

        public static DateTime LastDayOfWeek(DateTime date)
        {
            DateTime ldowDate = FirstDayOfWeek(date).AddDays(6);
            return ldowDate;
        }

        #endregion

        #region getDatatime UTC+7 hanoi vietname  FindSystemTimeZoneById = SE Asia Standard Time
        private DateTime getDateTimeHanoiVietName()
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime dateTime = DateTime.Now;
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, tst);
        }
        #endregion
    }
}
