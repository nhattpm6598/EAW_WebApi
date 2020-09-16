using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class NotificationRequest : IdRequest
    {
        public int ma { get; set; }
        public string content { get; set; }
        public string base64StringImg { get; set; }
        public int workshiftId { get; set; }
    }
}
