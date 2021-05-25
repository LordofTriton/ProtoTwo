using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProtoTwo.Models
{
    public class Login
    {
        //Necessary variables. Nothing else.

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string logStorage { get; set; }
        public static string error { get; set; }
    }
}