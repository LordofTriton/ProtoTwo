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
        /*public ActionResult Edit(Login std)
        {
            var UserName = std.UserName;
            var Password = std.Password;
            var standardName = std.standard.StandardName;

            return RedirectToAction("Index");
        }*/

        // GET: Login
        public ActionResult Index()
        {
            //fetch students from the DB using Entity Framework here

            return View();
        }

        /*public ActionResult Edit(string UserName, string Password)
        {
            //here, get the student from the database in the real application

            //getting a student from collection for demo purpose
            var std = Login.UserList
                .Where(s => s.UserName == UserName)
                .Where(s => s.UserName == UserName).FirstOrDefault();

            return View();
        }*/

        [HttpPost]
        public ActionResult Verify(string UserName, string Password)
        {
            UserDB udb = new UserDB();
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