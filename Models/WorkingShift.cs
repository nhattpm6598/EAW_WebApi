using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class WorkingShift
    {
        public WorkingShift()
        {
            CheckFace = new HashSet<CheckFace>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public bool Active { get; set; }
        public DateTime? CheckMin { get; set; }
        public DateTime? CheckMax { get; set; }
        public TimeSpan? TotalWorkTime { get; set; }
        public bool? Status { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public int? ModeAttendance { get; set; }
        public string UpdatePerson { get; set; }
        public int? ProcessingStatus { get; set; }
        public string Note { get; set; }
        public int StoreId { get; set; }
        public int TimeFramId { get; set; }
        public TimeSpan? BreakTime { get; set; }
        public DateTime? RequestedCheckOut { get; set; }
        public DateTime? RequestedCheckIn { get; set; }
        public int? IsRequested { get; set; }
        public string ApprovePerson { get; set; }
        public string NoteRequest { get; set; }
        public DateTime? LastCheckBeforeShift { get; set; }
        public DateTime? FirstCheckAfterShift { get; set; }
        public bool? IsOverTime { get; set; }
        public int? InMode { get; set; }
        public int? OutMode { get; set; }
        public int? BreakCount { get; set; }
        public TimeSpan? CheckInExpandTime { get; set; }
        public TimeSpan? CheckOutExpandTime { get; set; }
        public TimeSpan? ComeLateExpandTime { get; set; }
        public TimeSpan? LeaveEarlyExpandTime { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<CheckFace> CheckFace { get; set; }
    }
}
