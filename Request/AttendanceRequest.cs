using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class AttendanceRequest
    {
        [Required]
        public string EmpCode { get; set; }
        [Required]
        public string FaceMachineCode { get; set; }
        [Required]
        public string Mode { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime createTime { get; set; }
        [Required]
        public string WifiName { get; set; }


    }
}
