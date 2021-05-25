using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtoTwo.Models;

namespace ProtoTwo.Controllers
{
    public class WelcomeController : Controller
    {
        //Saves the user provided Rating into the DB
        [HttpPost]
        public ActionResult saveRating(string Rating)
        {
            Welcome mod = new Welcome();
            mod.SaveRating(Rating);

            return RedirectToAction("Index");
        }

        // GET: Welcome
        //URL: /Welcome/Index
        public ActionResult Index()
        {
            return View();
        }

        //URL: /Welcome/Appointment
        public ActionResult Appointment()
        {
            return View();
        }

        //URL: /Welcome/Feedback
        public ActionResult Feedback(string id)
        {
            ViewBag.feedid = "Rating";
            return View();
        }

        //URL: /Welcome/Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}