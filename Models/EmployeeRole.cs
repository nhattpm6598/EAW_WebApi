using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class EmployeeRole
    {
        public EmployeeRole()
        {
            EmployeeRoleList = new HashSet<EmployeeRoleList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int? Lever { get; set; }

        public virtual ICollection<EmployeeRoleList> EmployeeRoleList { get; set; }
    }
}
