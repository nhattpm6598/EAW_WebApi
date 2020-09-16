using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class EmployeeFace
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string EmpEnrollNumber { get; set; }
        public int FaceIndex { get; set; }
        public string FaceData { get; set; }
        public int? Type { get; set; }
        public string NameEmployeeInMachine { get; set; }
        public bool Active { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
