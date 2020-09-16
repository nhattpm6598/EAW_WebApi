using EAW_WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EAW_WebApi.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static AEW_DEVContext _context;

        public EmployeeRepository(AEW_DEVContext context)
        {
            _context = context;
        }
        #region table employee and role
        public Employee getDataEmpLoyeeAndRole(int id)
        {
            Employee employee = null;
            try
            {
                employee = _context.Employee.Where(e => e.Active == true && e.Id == id)
                    .Include(x => x.EmployeeRoleList)
                        .ThenInclude(y => y.EmpRole)
                    .FirstOrDefault();
            }
            catch(Exception ex)
            {
                Console.WriteLine("EmployeeRepository_getDataEmpLoyeeAndRole" + ex.Message);
            }
            return employee;
        }
        
        public Employee getDataEmployeeAndNotification(int id)
        {
            Employee employee = null;
            try
            {
                employee = _context.Employee.Where(e => e.Active == true && e.Id == id)
                    .Include(x => x.Notifications)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EmployeeRepository_getDataEmployeeAndNotification" + ex.Message);
            }
            return employee;
        }
        #endregion

        #region table employee and workshift
        public Employee getDataEmployeeAndWorkShit(int id)
        {
            Employee employee = null;
            try
            {
                employee = _context.Employee.Where(e => e.Active == true && e.Id == id)
                        .Include(x => x.WorkingShift)
                            .ThenInclude(y => y.Store)
                                .ThenInclude(z=>z.Brand)
                        .FirstOrDefault();
            }
            catch( Exception ex)
            {
                Console.WriteLine("EmployeeRepository_getDataEmployeeAndWorkShit" + ex.Message);
            }
           
            return employee;
        }
        #endregion

        #region update information Emp
        public string updateInfoOfEmp(int id,string key, string value)
        {
            var data = _context.Employee.Where(x => x.Id == id && x.Active == true).ToList();
            if(data.Count>0)
            {
                switch (key)
                {
                    case "birthday":
                        try
                        {
                            DateTime birth = Convert.ToDateTime(value);
                            data.ForEach(y => y.DateOfBirth = birth);
                        }
                        catch (FormatException)
                        {
                            return "datetime incorrect";
                        }
                        break;
                    case "phone":
                        data.ForEach(y => y.Phone = value);
                        break;
                    case "address":
                        data.ForEach(y => y.Address = value);
                        break;
                }
                return savedataBase();
            }
            else
            {
                return "notFound";
            }
        }

        private string savedataBase()
        {
            try
            {
                _context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("EmployeeRepository _ savedataBase" + ex.Message);
                return "failed";
            }
        }
        #endregion

        #region table employyee and notification
        public Employee getDataEmpAndNotfication(int id)
        {
            Employee employee = null;

            try
            {
                employee = _context.Employee.Where(e => e.Active == true && e.Id == id)
                    .Include(a => a.Notifications)
                        .ThenInclude(b=>b.CheckFace)
                            .ThenInclude(c=>c.WorkingShift)
                    .FirstOrDefault();
            }
            catch(Exception ex)
            {
                Console.WriteLine("EmployeeRepository_getDataEmpAndNotfication" + ex.Message);
            }

            return employee;
        }
        #endregion
    }
}
