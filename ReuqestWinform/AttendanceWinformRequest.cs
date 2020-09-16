using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.ReuqestWinform
{
    public class AttendanceWinformRequest
    {
 
        public string EmpCode { get; set; }
        public string FaceMachineCode { get; set; }
        public string Mode { get; set; }
        public string base64StringImg { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime createTime { get; set; }
    }
}
