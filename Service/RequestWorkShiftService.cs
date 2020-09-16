using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using EAW_WebApi.Service.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service
{
    public class RequestWorkShiftService : IRequestWorkShiftService
    {
        private UnitOfWork _unitOfWork;

        public RequestWorkShiftService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string insertRequest(WorkShiftRequest request)
        {
            return insertRequestInData(request);
        }


        #region insert request
        private string insertRequestInData(WorkShiftRequest request)
        {
            string json = null;

            //
            int empId = request.id;
            string nameWifi = request.nameWifi;
            string content = request.content;
            DateTime createTime = request.createTime;
            int storeId = getStoreIdByEmpId(empId);

            //new request
            CheckFace requestWorkShift = new CheckFace()
            {
                Active = true,
                EmployeeId = empId,
                StoreId = storeId,
                IpWifi = nameWifi,
                Content = content,
                Mode =3,//request
                Statuts = 1,//new
                CreateTime = createTime
            };
            
            _unitOfWork.Repository<CheckFace>().Insert(requestWorkShift);

            json =  _unitOfWork.Commit() >0 ? "success" : "failed";

            
            return json;
        }
        #endregion

        #region *** LINQ *** get dafault store by emp
        private int getStoreIdByEmpId(int empId)
        {
            return _unitOfWork.Repository<Employee>().Find(x => x.Active == true && x.Id == empId).MainStoreId;
        }
        #endregion
    }
}
