using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProtoTwo.Models
{
    public class Login
    {
        public static IList<Login> UserList = new List<Login>{
                new Login() { UserName = "John", Password = "pass" } ,
                new Login() { UserName = "Steve",  Password = "pass" } ,
                new Login() { UserName = "Bill",  Password = "pass" } ,
                new Login() { UserName = "Ram" , Password = "pass" } ,
                new Login() { UserName = "Ron" , Password = "pass" } ,
                new Login() { UserName = "Chris" , Password = "pass" } ,
                new Login() { UserName = "Rob" , Password = "pass" }
            };

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string logStorage { get; set; }
        //public static string error = "";
        public static string error { get; set; }
    }
}