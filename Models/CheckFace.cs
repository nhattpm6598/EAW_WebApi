using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class CheckFace
    {
        public CheckFace()
        {
            Notifications = new HashSet<Notifications>();
        }

        public int Id { get; set; }
        public int? FaceScanMachineId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime CreateTime { get; set; }
        public int? Mode { get; set; }
        public bool Active { get; set; }
        public int? StoreId { get; set; }
        public bool? IsUpdated { get; set; }
        public string Image { get; set; }
        public string IpWifi { get; set; }
        public string Gps { get; set; }
        public int? WorkShiftId { get; set; }
        public string Content { get; set; }
        public int? Statuts { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual FaceScanMachine FaceScanMachine { get; set; }
        public virtual WorkingShift WorkShift { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
