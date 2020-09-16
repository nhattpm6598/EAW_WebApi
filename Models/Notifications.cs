using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class Notifications
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Opening { get; set; }
        public string Content { get; set; }
        public bool? Active { get; set; }
        public int EmployeeId { get; set; }
        public int? CheckFaceId { get; set; }
        public string Creator { get; set; }

        public virtual CheckFace CheckFace { get; set; }
    }
}
