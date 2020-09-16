using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class InforRequest : IdRequest
    {

        [DataType(DataType.Date)]
        public DateTime? birth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public Int64 phone { get; set; }

        public string address { get; set; }

    }
}
