    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class UInforRequest : IdRequest
    {

        [DataType(DataType.Date)]
        public DateTime? birth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        public string address { get; set; }

    }
}
