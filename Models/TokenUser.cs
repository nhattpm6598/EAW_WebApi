using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class TokenUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FcmToken { get; set; }
        public string Uid { get; set; }
        public int? EmpId { get; set; }
        public string JwtToken { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
