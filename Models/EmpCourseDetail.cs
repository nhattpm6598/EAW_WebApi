using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class EmpCourseDetail
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int CourseId { get; set; }
        public int Status { get; set; }
        public int Certified { get; set; }
        public string CertifiedByName { get; set; }
        public bool Active { get; set; }
        public string Noted { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
