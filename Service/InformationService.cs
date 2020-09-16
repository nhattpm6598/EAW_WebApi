using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
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
    public class InformationService : Interface.IInformationService
    {
        private UnitOfWork _unitOfWork;
        public InformationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string getInformationEmpById(int id)
        {
            return getInfEmpById(id);
        }

        public string updateInformationEmpById(UInforRequest value)
        {
            return updateInfor(value);
        }


        #region get
        private string getInfEmpById(int id)
        {
            string json = null;
            JObject data = null;

            //access data
            Employee dataBase = getDataInforEmpByEmpId(id);

            //convert Jobject
            if (dataBase != null)
            {
                Brand brand = _unitOfWork.Repository<Brand>().Find(x => x.Active == true && x.Id == dataBase.BrandId);
                Store store = _unitOfWork.Repository<Store>().Find(x => x.Active == true && x.Id == dataBase.MainStoreId);
                //convert Jobject
                int lever = 0;
                string role = null;
                foreach (var items in dataBase.EmployeeRoleList)
                {
                    if (items.EmpRole.Lever > lever)
                    {
                        lever = int.Parse(items.EmpRole.Lever.ToString());
                        role = items.EmpRole.Name;
                    }
                }

                data = new JObject
                {
                    new JProperty("phone",dataBase.Phone),
                    new JProperty("email",dataBase.Email),
                    new JProperty("birthday",dataBase.DateOfBirth.HasValue ? dataBase.DateOfBirth : null),
                    new JProperty("address",dataBase.Address),
                    new JProperty("role",role),
                    new JProperty("store",store != null ? store.Name : null),
                    new JProperty("brand", brand != null ? brand.BrandName : null)   
                };

                //convert Jobject to json string 
                json = JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            
            return json;    
        }
        
        #region *** LINQ ***
        private Employee getDataInforEmpByEmpId(int empId)
        {
            return _unitOfWork.Repository<Employee>().GetAll().Where(e => e.Active == true && e.Id == empId)
                .Include(x => x.EmployeeRoleList)
                    .ThenInclude(y=>y.EmpRole)
                .FirstOrDefault();
        }
        #endregion

        #endregion

        #region update
        private string updateInfor(UInforRequest request)
        {
            //{"empId":"4","birthday":"1998-06-05"}
            //{"empId":"4","phone":"0826186206"}
            //{"empId":"4","address":"11/55d kp3 ptam hoa bien hoa dong nai"}
            int id = request.id;
            DateTime? birth = request.birth;
            string phone = request.phone;
            string address = request.address;

            if (birth != null)
            {
                var res = updateInfoOfEmp(id, "birthday", Convert.ToString(birth));
                return res;
            } else if (phone != null)
            {
                var res = updateInfoOfEmp(id, "phone", phone);
                return res;
            } else if (address != null)
            {
                var res = updateInfoOfEmp(id, "address", address);
                return res;
            }
            else
            {
                return null;
            }
            
        }

        #region ***LINQ*** update information Emp
        public string updateInfoOfEmp(int id, string key, string value)
        {
            Employee data = _unitOfWork.Repository<Employee>().Find(x => x.Id == id && x.Active == true);
            if (data != null)
            {
                switch (key)
                {
                    case "birthday":
                        try
                        {
                            DateTime birth = Convert.ToDateTime(value);
                            data.DateOfBirth = birth;
                        }
                        catch (FormatException)
                        {
                            return "datetime incorrect";
                        }
                        break;
                    case "phone":
                        data.Phone = value;
                        break;
                    case "address":
                        data.Address = value;
                        break;
                }
                return  _unitOfWork.Commit() > 0  ? "success" : "failed";
            }
            else
            {
                return "notFound";
            }
        }

        
        #endregion

        #endregion

    }
}
