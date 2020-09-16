using EAW_WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Data.Repository
{
    interface IEmployeeRepository 
    {
        Employee getDataEmployeeAndWorkShit(int id);

        Employee getDataEmpLoyeeAndRole(int id);

        Employee getDataEmployeeAndNotification(int id);

        string updateInfoOfEmp(int id, string key, string value);
    }
}
