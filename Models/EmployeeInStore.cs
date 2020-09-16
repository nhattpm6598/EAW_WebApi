using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class EmployeeInStore
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public DateTime AssignedDate { get; set; }
        public int? Status { get; set; }
        public bool Active { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Store Store { get; set; }
    }
}
