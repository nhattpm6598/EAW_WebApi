using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class EmployeeRoleList
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int EmpRoleId { get; set; }
        public DateTime? BeginDay { get; set; }
        public DateTime? FinishDay { get; set; }
        public bool Active { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual EmployeeRole EmpRole { get; set; }
    }
}
