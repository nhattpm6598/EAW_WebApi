using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.ServiceAdmin.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.ServiceAdmin
{
    public class EmployeeService : IEmployeeService
    {
        private static UnitOfWork _unitOfWork = null;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string getTotalHourOfEmployeeByStore()
        {
            return getTotalTimeEmployeeinWeek();
        }

        #region get Total Hour employee By Store id
        private string getTotalTimeEmployeeinWeek()
        {
            string json = null;

            //init
            DateTime nowDate = getDateTimeHanoiVietName();
            DateTime firstOfWeek = FirstDayOfWeek(nowDate);
            DateTime lastOfWeek = LastDayOfWeek(nowDate);

            //access data
            List<Employee> database = getEmployeeinStore();
            if (database.Count > 0)
            {
                JArray array = new JArray();
                foreach (var employee in database)
                {
                    double totalTimeDouble = 0;
                    foreach (var workShift in employee.WorkingShift)
                    {
                        #region total hour
                        if (DateTime.Compare(workShift.ShiftStart.Date, lastOfWeek.Date) <= 0 && DateTime.Compare(workShift.ShiftStart.Date, firstOfWeek.Date) >= 0)
                        {
                            //nhưng ngày nhỏ hơn nowday và có status là true (đã duoc xác nhân cua manager)
                            if (DateTime.Compare(workShift.ShiftStart.Date, nowDate.Date) <= 0 && workShift.Status == true)
                            {
                                TimeSpan time = TimeSpan.Parse(workShift.TotalWorkTime.ToString());
                                totalTimeDouble += time.TotalHours;
                            }
                        }
                        #endregion
                    }
                    //end workshift
                    JObject newEmp = new JObject( 
                        new JProperty("startWeek",firstOfWeek.ToString("d")),
                        new JProperty("now",nowDate.ToString("d")),
                        new JProperty("endWeek",lastOfWeek.ToString("d")),
                        new JProperty("StoreId",getNameStoreByStoreId(employee.MainStoreId)),
                        new JProperty("id",employee.Id),
                        new JProperty("name",employee.Name),
                        new JProperty("TotalTime",totalTimeDouble)
                        );
                    array.Add(newEmp);
                }
                //
                json = JsonConvert.SerializeObject(array, Formatting.Indented);
            }
            
            return json;
        }
        #endregion

        #region *** LINQ ***
        private List<Employee> getEmployeeinStore()
        {
            return _unitOfWork.Repository<Employee>().GetAll().Where(s => s.Active == true)
                .Include(x=>x.WorkingShift)
                .ToList();
        }
        private string getNameStoreByStoreId(int storeId)
        {
            return _unitOfWork.Repository<Store>().Find(x => x.Id == storeId).Name;
        }
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
