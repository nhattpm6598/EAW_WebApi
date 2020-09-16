using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class ScheduleRequest : IdRequest
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime firstDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime lastDate { get; set; }
    }
}
