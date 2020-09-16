using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using EAW_WebApi.ReuqestWinform;
using EAW_WebApi.Service.Interface;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EAW_WebApi.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly static string title = "Attendance";
        private readonly static string success = "Hệ thống điểm danh thành công";
        private readonly static string imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTc2SlQt05rDOzr2jaoFR__8mcpdJUCNckk5g&usqp=CAU";
        private static UnitOfWork _unitOfWork = null;
        public AttendanceService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Message CreateMessageOfWinform(AttendanceWinformRequest request)
        {
            return CreateMess(request.EmpCode,request.FaceMachineCode,request.Mode,request.base64StringImg,request.createTime,null);
        }

        public Message CreateMessageOfFutter(AttendanceRequest request)
        {
            return CreateMess(request.EmpCode, request.FaceMachineCode, request.Mode, null, request.createTime, request.WifiName);
        }

        #region create mess
        // create message to android
        private Message CreateMess(string code, string machineCode, string mode,string base64, DateTime dateTime, string wifiName)
        {
            Message message = null;

            string EmpCode = code;
            DateTime createTime = dateTime;

            Employee emp = _unitOfWork.Repository<Employee>().Find(x => x.EmployeeCode == EmpCode);
            
            var tokenUser = _unitOfWork.Repository<TokenUser>().Find(t => t.Username == emp.Email);
            
            if (RecordHistory(code,machineCode,mode,base64,dateTime,wifiName) == true)
            {
                // See documentation on defining a message payload.
                message = new Message()
                {
                    Data = new Dictionary<string, string>()
                    {
                        { "title", title },
                        { "content", success }, 
                        { "time", createTime.ToString() }
                    },
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = success,
                        ImageUrl = imageUrl
                    },  
                    Token = tokenUser.FcmToken
                };
            }

            return message;
        }
        #endregion

        #region recordHistory
        private bool RecordHistory(string code, string machineCode, string mode,string base64, DateTime dateTime, string wifiName)
        {
            if(insertNewCheckFace(code,machineCode,mode,base64,dateTime,wifiName) == true)
            {
               // Console.WriteLine("AttendanceService _ updateWorkShiftAttendance"+ updateWorkShiftAttendance(checkFace)); 
                return insertNewNotification(checkFace);
            }

            return false;
        }
        #endregion

        #region insert new checkface
        private static CheckFace checkFace { get; set; }

        private bool insertNewCheckFace(string code, string machineCode, string mode,string base64, DateTime dateTime, string wifiName)
        {
            // *** LINQ *** find employee by email *** database ***
            Employee employee = _unitOfWork.Repository<Employee>().Find(x => x.EmployeeCode == code);
            // *** LINQ *** find FaceMachine by machinecode *** database ***
            FaceScanMachine faceScanMachine = _unitOfWork.Repository<FaceScanMachine>().Find(x => x.MachineCode == machineCode);

            //init checkface table and notification table
            if(employee != null && faceScanMachine != null)
            {
                int modeData = int.Parse(mode);
                checkFace = new CheckFace()
                {
                    Active = true,
                    EmployeeId = employee.Id,
                    FaceScanMachineId = faceScanMachine.Id,
                    Mode = modeData,
                    StoreId = faceScanMachine.StoreId,
                    CreateTime = dateTime,
                    Image = base64 != null ? base64 : null,
                    Statuts = 1,//new
                    IpWifi = wifiName
                };

                _unitOfWork.Repository<CheckFace>().Insert(checkFace);

                return _unitOfWork.Commit() > 0 ? true : false;
            }
            return false;
        }
        #endregion

        #region insert new notification
        private bool insertNewNotification(CheckFace checkFace)
        {
            //find checkfaceId by empId and machineId
            List<CheckFace> checkFaces = _unitOfWork.Repository<CheckFace>().GetAll().Where(x => x.EmployeeId == checkFace.EmployeeId && x.FaceScanMachineId == checkFace.FaceScanMachineId).ToList();
            if(checkFaces != null)
            {
                foreach(var items in checkFaces)
                {
                    if (items.CreateTime == checkFace.CreateTime)
                    {
                        Notifications notification = new Notifications()
                        {
                            Active = true,
                            EmployeeId = checkFace.EmployeeId.Value,
                            CheckFaceId = items.Id,
                            CreateDate = checkFace.CreateTime,
                            Title = title,
                        };

                        _unitOfWork.Repository<Notifications>().Insert(notification);

                        return _unitOfWork.Commit() > 0 ? true : false;
                    }
                }
            }
            
            return false;
        }


        #endregion

        #region update Attendace of Workshift
        private bool updateWorkShiftAttendance(CheckFace checkFace)
        {
            int workShiftId = checkTimeAttendace(checkFace);
            if (workShiftId != -1)
            {
                // *** LINQ *** get row data workshift update 
                WorkingShift working = _unitOfWork.Repository<WorkingShift>().Find(x => x.Active == true && x.Id == workShiftId);
                //update status -  checkTime - modeAttendance 
                working.Status = true;
                working.CheckInExpandTime = checkFace.CreateTime.TimeOfDay;
                working.ModeAttendance = checkFace.Mode;
                // *** LINQ *** save change data 
                return _unitOfWork.Commit() > 0;

            }
            return false;
        }
        //check time attendance
        private int checkTimeAttendace(CheckFace checkFace)
        {
            // *** LINQ *** get all data workingShift by employee Id in checkFace
            List<WorkingShift> workings = _unitOfWork.Repository<WorkingShift>().GetAll().Where(x => x.Active == true && x.EmployeeId == checkFace.EmployeeId).ToList();
            //
            foreach (var items in workings)
            {
                
                if(DateTime.Compare(items.CheckMin.Value,checkFace.CreateTime)<=0 && DateTime.Compare(items.CheckMax.Value, checkFace.CreateTime) >= 0)
                {
                    return items.Id;
                }
            }

            return -1; 
        }

        #endregion
    }
}
