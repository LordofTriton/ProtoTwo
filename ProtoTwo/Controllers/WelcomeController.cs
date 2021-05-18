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
        [HttpPost]
        public ActionResult sendMessage(Welcome zmodel)
        {
            //send email to specified address using user-provided data

            return RedirectToAction("Contact");
        }

        [HttpPost]
        public ActionResult saveRating(string Rating)
        {
            Welcome mod = new Welcome();
            mod.SaveRating(Rating);

            return RedirectToAction("Index");
        }

        // GET: Welcome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Appointment()
        {
            return View();
        }

        public ActionResult Feedback(string id)
        {
            ViewBag.feedid = "Rating";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}