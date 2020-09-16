using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EAW_WebApi.Request
{
    public class loginRequest
    {
        [Required(ErrorMessage ="{0} is required")]
        public string firebaseToken { get; set; }
        [Required(ErrorMessage ="{0} is required")]
        public string tokenFcm { get; set; }
    }
}
