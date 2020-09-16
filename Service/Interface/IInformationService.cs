using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Service.Interface
{
    interface IInformationService
    {
        string getInformationEmpById(int id);

        string updateInformationEmpById(UInforRequest request);
    }
}
