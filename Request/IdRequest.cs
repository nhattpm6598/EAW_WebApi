using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class IdRequest
    {
        [Required]
        public int id { get; set; }
    }
}
