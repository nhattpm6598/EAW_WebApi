using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class AttendanceDate
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public DateTime DateReport { get; set; }
        public int TotalEmployee { get; set; }
        public int TotalOnTimeEmployee { get; set; }
        public int TotalSession { get; set; }
        public int TotalOnTimeSession { get; set; }
        public int TotalMissSession { get; set; }
        public int TotalMissEmployee { get; set; }
        public int TotalComeLate { get; set; }
        public int TotalComeOnTime { get; set; }
        public int TotalReturnEarly { get; set; }
        public int TotalReturnOntime { get; set; }
        public TimeSpan TotalWorkingTime { get; set; }
        public int Status { get; set; }
        public bool Active { get; set; }

        public virtual Store Store { get; set; }
    }
}
