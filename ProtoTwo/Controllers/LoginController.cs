using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtoTwo.Models;

namespace ProtoTwo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //URL: /Login/Index
        public ActionResult Index()
        {
            return View();
        }

        //Verifies that user provided details exist in the Db
        [HttpPost]
        public ActionResult Verify(string UserName, string Password)
        {
            UserDB udb = new UserDB(); //Specialized class for dealing with the User Database

            if (udb.Verify(UserName, Password))
            {
                ViewBag.currentUser = UserName;
                return RedirectToAction("Dashboard", "Main");
            } else
            {
                const string errorMsg = "Incorrect Username or Password!";
                Login.error = errorMsg;
                return RedirectToAction("Index");
            }
        }
    }
}