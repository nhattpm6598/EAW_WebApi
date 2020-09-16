using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using EAW_WebApi.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service
{
    public class HistoryService : IHistoryService
    {
        private UnitOfWork _unitOfWork;
        public HistoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string getData(ScheduleRequest request)
        {
            return getDataHisAttendance(request);
        }

        #region getDataHisAttendance 
        private string getDataHisAttendance(ScheduleRequest request)
        {
            //init
            string json = null;
            int id = request.id;
            DateTime first = request.firstDate;
            DateTime last = request.lastDate;
            DateTime nowDate = DateTime.Now;

            // check start date && end date
            bool checkres = true;
            if (first >= nowDate)
            {
                checkres = false;
            }
            else if (last >= nowDate)
            {
                last = nowDate;
            }


            //if = true => 
            if (checkres == true)
            {
                //access data 
                List<CheckFace> dataBase = getDataCheckFaceByEmpid(id);

                //convert logic
                if (dataBase != null)
                {
                    JArray array = new JArray();

                    List<CheckFace> listData = dataBase.OrderByDescending(x => x.CreateTime).ThenByDescending(x => x.CreateTime).ToList();

                    foreach (var items in listData)
                    {
                        
                        if (items.CreateTime.Date <= last && items.CreateTime.Date >= first)
                        {
                            JObject tmp = new JObject
                            (
                                new JProperty("date", items.CreateTime.DayOfWeek + "\n" + items.CreateTime.Day + "-" + items.CreateTime.Month),
                                new JProperty("time", items.CreateTime.ToString("H:mm")),
                                new JProperty("Store", items.StoreId.HasValue ? getDataNameStore(items.StoreId.Value) : null),
                                new JProperty("mode", items.Mode == 1 ? "camera" : (items.Mode == 2 ? "QRcode" : (items.Mode == 3 ? "request" : null))),
                                new JProperty("status", items.Statuts == 1 ? "new" : (items.Statuts == 2 ? "Approved" : (items.Statuts == 3 ? "Reject" : null))),
                                new JProperty("image", items.Mode == 1 ? items.Image : null)
                            ) ;
                            array.Add(tmp);
                        }
                    }
                    JObject data = new JObject(new JProperty("HistoryAttendance", array));
                    // covert jobject to json
                    json = JsonConvert.SerializeObject(data, Formatting.Indented);
                }
            }

            return json;
        }
        #endregion

        #region *** LINQ *** getDataHistoryAttendance
        private List<CheckFace> getDataCheckFaceByEmpid(int empId)
        {
            return _unitOfWork.Repository<CheckFace>().GetAll().Where(x => x.Active == true && x.EmployeeId == empId)
                .Include(y=>y.FaceScanMachine)
                    .ThenInclude(z=>z.Store)
                .ToList();
        }
        #endregion

        #region *** LINQ *** getStoreName
        private string getDataNameStore(int storeID)
        {
            return _unitOfWork.Repository<Store>().Find(x => x.Id == storeID).Name;
        }
        #endregion
    }
}
