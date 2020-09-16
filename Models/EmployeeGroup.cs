using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class EmployeeGroup
    {
        public EmployeeGroup()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string CodeGroup { get; set; }
        public string NameGroup { get; set; }
        public int BrandId { get; set; }
        public bool Active { get; set; }
        public TimeSpan? ExpandTime { get; set; }
        public int? GroupPolicy { get; set; }
        public int? GroupSecurity { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
